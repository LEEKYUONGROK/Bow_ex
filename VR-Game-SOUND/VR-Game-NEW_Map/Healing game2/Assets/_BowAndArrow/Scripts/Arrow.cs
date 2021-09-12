using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [Header("Settings")]
    public float speed = 2000.0f;

    [Header("Hit")]
    public Transform tip = null;
    public LayerMask layerMask = ~Physics.IgnoreRaycastLayer;

    private new Collider collider = null;
    private new Rigidbody rigidbody = null;

    private Vector3 lastPosition = Vector3.zero;
    private bool launched = false;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Do this first, so we get the right physics values
        if (args.interactor is XRDirectInteractor)
            Clear();

        // Make sure to do this
        base.OnSelectEntering(args);
    }

    private void Clear()
    {
        SetLaunch(false);
        TogglePhysics(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Make sure to do this
        base.OnSelectExited(args);

        // If it's a notch, launch the arrow
        if (args.interactor is Notch notch)
            Launch(notch);
    }

    private void Launch(Notch notch)
    {
        // Double-check incase the bow is dropped with arrow socketed
        if (notch.IsReady)
        {
            SetLaunch(true);
            UpdateLastPosition();
            ApplyForce(notch.PullMeasurer);
        }
    }

    private void SetLaunch(bool value)
    {
        collider.isTrigger = value;
        launched = value;
    }

    private void UpdateLastPosition()
    {
        // Always use the tip's position
        lastPosition = tip.position;
    }

    private void ApplyForce(PullMeasurer pullMeasurer)// 마지막 위치에서 화살이 힘을 받아 날아가는걸 구현하는 함수
    {
        // Apply force to the arrow
        float power = pullMeasurer.PullAmount;
        Vector3 force = transform.forward * (power * speed);
        rigidbody.AddForce(force);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)//XRGrabInteractable의 기존함수 ProcessInteractable를 재정의
    {                                                                                          //XRInteractionUpdateOrder.UpdatePhase를 매게 변수로 받음
        base.ProcessInteractable(updatePhase);//기존함수를 그대로 작동 시키고 

        if (launched)//추가 부분
        {
            // 충돌확인 
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)// XRInteractionUpdateOrder.UpdatePhase.Dynamic = 매 프레임 마다 호출
            {
                if (CheckForCollision())//화살이 충돌이 일어나면
                    launched = false;//화살이 나감을 체크

                UpdateLastPosition();// 마지막위치체크 
            }

            // 각 물리 업데이트와 함께 방향만 설정
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed) // 프레임 속도 고정
                SetDirection();
        }
    }

    private void SetDirection()
    {
        // 화살표가 움직이는 방향을 봐
        if (rigidbody.velocity.z > 0.5f)
            transform.forward = rigidbody.velocity;
    }

    private bool CheckForCollision()
    {
        // 히트가 있었는지 확인
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hit, layerMask))
        {
            TogglePhysics(false);
            ChildArrow(hit);
            CheckForHittable(hit);
        }

        return hit.collider != null;
    }

    private void TogglePhysics(bool value)
    {
        // Disable physics for childing and grabbing
        rigidbody.isKinematic = !value;
        rigidbody.useGravity = value;
    }

    private void ChildArrow(RaycastHit hit)
    {
        // Child to hit object
        Transform newParent = hit.collider.transform;
        transform.SetParent(newParent);
    }

    private void CheckForHittable(RaycastHit hit)
    {
        // Check if the hit object has a component that uses the hittable interface
        //적중 개체에 적중 가능한 인터페이스를 사용하는 구성 요소가 있는지 확인하십시오.
        GameObject hitObject = hit.transform.gameObject;
        IArrowHittable hittable = hitObject ? hitObject.GetComponent<IArrowHittable>() : null;

        //유효한 구성 요소를 찾으면 해당 구성 요소에 있는 기능을 호출합니다.
        if (hittable != null)
            hittable.Hit(this);
    }
}
