using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Dictionary<Item, int> items;

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
    }

    public void RemoveItem(Item item)
    {
        items[item]--;
        if (items[item] == 0)
        {
            items.Remove(item);
        }
    }
}
