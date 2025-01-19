using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private ItemEffectDatabase theItemEffectDatabase;

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        

        if (item.itemType != Item.ItemType.Equipment)
        {
            text_Count.text = itemCount.ToString();
            go_CountImage.SetActive(true);
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        
        SetColor(1f);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0f);

        go_CountImage.SetActive(false);
        text_Count.text = "0";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                theItemEffectDatabase.UseItem(item);
                if (item.itemType == Item.ItemType.Used)
                {
                    SetSlotCount(-1);
                }
                
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.SetColor(1f);
            DragSlot.instance.transform.position = eventData.position;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    { 
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag 호출");
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.SetColor(0);
    }


    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot(); 
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop 호출");
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theItemEffectDatabase.ShowToolTop(item, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theItemEffectDatabase.HideToolTip();
    }
}

