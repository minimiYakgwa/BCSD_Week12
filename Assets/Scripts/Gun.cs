using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;
    public float accuracy;
    public float fireRate;
    public float reloadTime;

    public int damage;
    public int reloadBulletCount;
    public int currentBulletCount;
    public int maxBulletCount;
    public int carryBulletCount;

    public float retroActionForce;
    public float retroActioniFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
    
}
