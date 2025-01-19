using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;


[System.Serializable]
public class ItemEffect
{
    public string itemName;
    [Tooltip("HP, SP, DP, HUNGRY, THIRSTY, SATISFY만 가능합니다.")]
    public string[] part;
    public int[] num;
}
public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;
    [SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private WeaponManager theWeaponManager;
    [SerializeField]
    private SlotTooltip theSlotTooltip;

    private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void ShowToolTop(Item _item, Vector3 _pos)
    {
        theSlotTooltip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theSlotTooltip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHP(itemEffects[x].num[y]);
                                break;
                            case SP:
                                thePlayerStatus.IncreaseSP(itemEffects[x].num[y]);
                                break;
                            case DP:
                                thePlayerStatus.IncreaseDP(itemEffects[x].num[y]);
                                break;
                            case HUNGRY:
                                thePlayerStatus.IncreaseHungry(itemEffects[x].num[y]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.IncreaseThirsty(itemEffects[x].num[y]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("잘못된 부위를 적용하려 하고 있습니다. HP, SP, DP, HUNGRY, THIRSTY, SATISFY만 가능합니다.");
                                break;
                        }
                        Debug.Log(_item.itemName + " 을 " + itemEffects[x].part[y] + "에 사용하였습니다. ");
                    }
                    return;
                }
            }
            Debug.Log("일치하는 itemName이 데이터베이스에 없습니다.");
        }
    }
}
