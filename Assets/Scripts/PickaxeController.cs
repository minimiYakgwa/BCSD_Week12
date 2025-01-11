using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    public static bool isActivate = false;

    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }
    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = !isSwing;
                Debug.Log(hitinfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _hand)
    {
        base.CloseWeaponChange(_hand);
        isActivate = true;
    }
}
