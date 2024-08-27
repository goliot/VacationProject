using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    public static Inventory instance;

    // ������ ����� ������ ����Ʈ
    public List<string> items = new List<string>();
    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        instance = this;
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    private void OnEnable()
    {
        int idx = 0;

        foreach(Slot slot in slots)
        {
            slot.slotNum = idx;
            idx++;
        }
    }

    public void Init()
    {
        int idx = 0;
        // TODO : ���� �÷��̾ ���� �����۵��� �ҷ�����
        PlayerItems playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
        foreach(KeyValuePair<Item, int> item in playerItems.items)
        {
            // TODO: �κ��丮�� �ֱ�
            slots[idx].item = item.Key;
            slots[idx].UpdateSlotUI();
        }
    }

    // ������ �߰� �޼���
    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log(item + "��(��) �κ��丮�� �߰��Ǿ����ϴ�.");
    }

    // ������ ���� �޼���
    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item + "��(��) �κ��丮���� ���ŵǾ����ϴ�.");
        }
        else
        {
            Debug.LogWarning(item + "��(��) �κ��丮�� �����ϴ�.");
        }
    }
}
