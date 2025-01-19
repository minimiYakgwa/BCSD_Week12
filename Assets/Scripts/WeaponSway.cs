using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private Vector3 originPos;

    private Vector3 currentPos;

    [SerializeField]
    private Vector3 limitPos;


    [SerializeField]
    private Vector3 fineSightLimitPos;

    [SerializeField]
    private Vector3 smoothSway;

    [SerializeField]
    private GunController theGunController;

    private void Start()
    {
        originPos = this.transform.localPosition;
    }

    private void Update()
    {
        if (!Inventory.inventoryActivated)
            TrySway();
    }

    private void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            Swaying();
        }
        else
        {
            BackToOriginPos();
        }
    }

    private void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");

        if (!theGunController.isFineSightMode)
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limitPos.x, limitPos.x),
                       Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -limitPos.y, limitPos.y), originPos.z);
        }
        else
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -fineSightLimitPos.x, fineSightLimitPos.x),
                       Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -fineSightLimitPos.y, fineSightLimitPos.y), originPos.z);
        }
        transform.localPosition = currentPos;
        
    }

    private void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}
