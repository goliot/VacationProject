using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    public static Inventory instance;

    public Slot[] slots;
    public Transform slotHolder;
    public PlayerItems playerItems;

    private void Start()
    {
        instance = this;
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    private void OnEnable()
    {
        playerItems = GameManager.Instance.player.GetComponent<PlayerItems>();

        // ������ ���� �̺�Ʈ ����
        playerItems.OnItemsChanged += UpdateInventoryUI;

        int idx = 0;
        foreach (Slot slot in slots)
        {
            slot.slotNum = idx;
            idx++;
        }

        // �κ��丮 UI �ʱ�ȭ
        UpdateInventoryUI();

        ClearSlots();
    }

    private void OnDisable()
    {
        // �κ��丮�� ��Ȱ��ȭ�� �� �̺�Ʈ ���� ����
        if (playerItems != null)
        {
            playerItems.OnItemsChanged -= UpdateInventoryUI;
        }
    }

    public void UpdateInventoryUI()
    {
        int idx = 0;
        foreach (KeyValuePair<Item, int> item in playerItems.items)
        {
            if (idx < slots.Length)
            {
                slots[idx].item = item.Key;
                slots[idx].itemCount = item.Value;
                slots[idx].UpdateSlotUI();
                idx++;
            }
        }
    }

    public void ClearSlots()
    {
        foreach (Slot slot in slots)
        {
            slot.RemoveSlot();
        }
    }
}
