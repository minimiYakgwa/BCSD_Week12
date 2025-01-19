using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private TextMeshProUGUI actionText;

    [SerializeField]
    private Inventory theInventory;

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        actionText.gameObject.SetActive(true);
        pickupActivated = true;
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " »πµÊ " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (hitInfo.transform != null)
        {
            Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " »πµÊ«ﬂΩ¿¥œ¥Ÿ.");
            theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
            Destroy(hitInfo.transform.gameObject);
            InfoDisappear(); 
        }
    }
}
