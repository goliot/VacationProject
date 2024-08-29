using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Dictionary<Item, int> items;

    // 아이템이 추가되거나 제거될 때 발생하는 이벤트
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

        // 이벤트 호출
        OnItemsChanged?.Invoke();
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
            OnItemsChanged?.Invoke();
        }
    }
}
