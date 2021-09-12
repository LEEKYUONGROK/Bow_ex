using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_JumpController : MonoBehaviour
{
    public XRNode inputSource;
    private InputDevice device;
    public AudioClip jumpSound;
    public LayerMask CeilingLayer;
    public float jumpHeight = 5.0F;
    public bool isJumping = false;
    private bool lastState = false;
    private float jumppos;
    public float jumpSpeed = 4.0f;

    private float fallingSpeed;
    private XRRig rig;
    private AudioSource MainSFX;
    private Vector2 inputAxis;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        MainSFX = GetComponent<AudioSource>();
        MainSFX.clip = jumpSound;
        InitController();
    }

    void InitController()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }


    // Update is called once per frame
    void Update()
    {
        if (!device.isValid)
            InitController();


        if (SecondaryButtonDown() )
        {
            isJumping = true;
            MainSFX.Play();
            jumppos = character.transform.position.y;
            character.slopeLimit = 90; // fixes the jump directly in fornt of thing while pressing forward. has to be reset to 45 when isJumping = false
        }

        if (isJumping)
            Jump();

        if (HitCeiling())
            isJumping = false;


    }

    public bool HitCeiling()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.1f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.up, out RaycastHit hitInfo, rayLength, CeilingLayer);
        return hasHit;
    }



    private void Jump()
    {
        if (character.transform.position.y >= jumppos + jumpHeight)
        {
            isJumping = false;
            character.slopeLimit = 45;
        }
        character.Move(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
    }

    public bool SecondaryButtonDown()
    {
        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            bool tempStatePrimary = secondaryButtonValue;

            if (tempStatePrimary != lastState)  //Button Down
            {
                lastState = tempStatePrimary;
                return true;
            }
        }
        else
        {
            lastState = false;
        }
        return false;
    }

}
