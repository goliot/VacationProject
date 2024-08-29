using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Dictionary<Item, int> items;

    // �������� �߰��ǰų� ���ŵ� �� �߻��ϴ� �̺�Ʈ
    public event Action OnItemsChanged;

    private void Awake()
    {
        items = new Dictionary<Item, int>();
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
        OnItemsChanged?.Invoke();
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
            OnItemsChanged?.Invoke();
        }
    }
}
