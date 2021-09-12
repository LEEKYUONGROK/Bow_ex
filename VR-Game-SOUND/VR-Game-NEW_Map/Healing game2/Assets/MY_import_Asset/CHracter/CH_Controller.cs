using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_Controller : MonoBehaviour
{
    public static Animator CH_animator;
    private void Awake()
    {
        CH_animator = this.GetComponent<Animator>();
    }
    public void animation_Idle()
    {
        CH_animator.SetInteger("Ch_Animation", 0);
    }
    public void animation_Walk()
    {
        Debug.Log("°È±â");
        CH_animator.SetInteger("Ch_Animation", 1);
    }
    public void animation_ReLoad()
    {
        Debug.Log("½É±â");
        CH_animator.SetInteger("Ch_Animation", 2);
    }
    public void animation_Shot()
    {
        Debug.Log("È°½î±â");
        CH_animator.SetInteger("Ch_Animation", 4);
    }

}
