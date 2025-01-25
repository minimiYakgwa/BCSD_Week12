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
                if (hitinfo.transform.tag == "Rock")
                {
                    hitinfo.transform.GetComponent<Rock>().Mining();
                }
                else if (hitinfo.transform.tag == "WeakAnimal")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitinfo.transform.GetComponent<WeakAnimal>().Damage(1, transform.position);
                }
                else if (hitinfo.transform.tag == "StrongAnimal")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitinfo.transform.GetComponent<WeakAnimal>().Damage(1, transform.position);
                }

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
