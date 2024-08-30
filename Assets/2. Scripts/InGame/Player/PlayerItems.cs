using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    // �������� �߰��ǰų� ���ŵ� �� �߻��ϴ� �̺�Ʈ
    public event Action OnInventoryItemsChanged;
    public event Action OnEquipmentItemsChanged;

    public Dictionary<Item, int> items; // ������, ����
    public Dictionary<ItemType, Item> equipments; // ����, ������

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

        // �̺�Ʈ ȣ��
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

            // ������ ������ 0�� �Ǹ� ��ųʸ����� ����
            if (items[item] == 0)
            {
                items.Remove(item);
            }

            // �̺�Ʈ ȣ��
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
