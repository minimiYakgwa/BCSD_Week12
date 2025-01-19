using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotTooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;
    [SerializeField]
    private TextMeshProUGUI txt_ItemHowToUsed;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width / 2, -go_Base.GetComponent<RectTransform>().rect.height / 2, 0f);
        go_Base.transform.position = _pos;

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
        {
            txt_ItemHowToUsed.text = "우클릭 - 장착";
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            txt_ItemHowToUsed.text = "우클릭 - 먹기";
        }
        else
        {
            txt_ItemHowToUsed.text = "";
        }
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
