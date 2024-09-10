using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item;
    public Image itemIcon;
    public int itemCount;
    public TextMeshProUGUI itemCountText;

    public virtual void UpdateSlotUI()
    {
        if (item == null) return;
        else
        {
            itemIcon.sprite = item.itemImage;
            itemIcon.gameObject.SetActive(true);
            itemCountText.text = itemCount.ToString();
            if (itemCount <= 0)
            {
                RemoveSlot();
            }
        }

        //if(item.isEquipmentItem)
        //{
        //    canDrag = true;
        //}
        //else
        //{
        //    canDrag = false;
        //}
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnClickButton()
    {
        if (item == null) return;
        item.UseItem();

        UpdateSlotUI();
    }
}
