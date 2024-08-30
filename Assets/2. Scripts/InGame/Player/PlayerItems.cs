using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    // 아이템이 추가되거나 제거될 때 발생하는 이벤트
    public event Action OnInventoryItemsChanged;
    public event Action OnEquipmentItemsChanged;

    public Dictionary<Item, int> items; // 아이템, 개수
    public Dictionary<ItemType, Item> equipments; // 부위, 아이템

    private void Awake()
    {
        items = new Dictionary<Item, int>();
        equipments = new Dictionary<ItemType, Item>();
    }

    public void InsertItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            items[item]++;
        }
        else
        {
            items[item] = 1;
        }

        // 이벤트 호출
        OnInventoryItemsChanged?.Invoke();
    }

    public void EquipItem(ItemType type, Item item)
    {
        equipments[type] = item;

        OnEquipmentItemsChanged?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (items.ContainsKey(item) && items[item] > 0)
        {
            items[item]--;

            // 아이템 개수가 0이 되면 딕셔너리에서 제거
            if (items[item] == 0)
            {
                items.Remove(item);
            }

            // 이벤트 호출
            OnInventoryItemsChanged?.Invoke();
        }
    }

    public void UnequipItem(ItemType type)
    {
        if(equipments.ContainsKey(type))
        {
            InsertItem(equipments[type]);
            equipments.Remove(type);

            OnEquipmentItemsChanged?.Invoke();
        }
    }
}
