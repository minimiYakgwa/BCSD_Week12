using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName;

    public bool isHand;
    public bool isAxe;
    public bool isPickaxe;


    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay;
    public float attackDelayA;
    public float attackDelayB;

    public Animator anim;
}
