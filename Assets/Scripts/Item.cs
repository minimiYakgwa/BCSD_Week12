using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemImage;
    public ItemType itemType;
    public GameObject itemPrefab;


    public string weaponType;

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

}
