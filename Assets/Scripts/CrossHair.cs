using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GunController theGunController;

    private float gunAccuracy;

    [SerializeField]
    private GameObject go_CrossharHud;

    [SerializeField]

    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        anim.SetBool("Walking", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        anim.SetBool("Running", _flag);
    }
    public void JumppingAnimation(bool _flag)
    {
        anim.SetBool("Running", _flag);
    }
    public void CrouchingAnimation(bool _flag)
    {
        anim.SetBool("Crouching", _flag);
    }

    public void FineSightAnimation(bool _flag)
    {
        anim.SetBool("FineSight", _flag);
    }

    public void FireAnimation()
    {
        if (anim.GetBool("Walking"))
            anim.SetTrigger("Walk_Fire");
        else if (anim.GetBool("Crouching"))
            anim.SetTrigger("Crouch_Fire");
        else
            anim.SetTrigger("Idle_Fire");
    }

    public float GetAcurracy()
    {
        if (anim.GetBool("Walking"))
            gunAccuracy = 0.08f;
        else if (anim.GetBool("Crouching"))
            gunAccuracy = 0.02f;
        else if (theGunController.GetFineSightMode())
            gunAccuracy = 0.0001f;
        else
            gunAccuracy = 0.04f;
        return gunAccuracy;
    }
}
