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

        // 아이템 변경 이벤트 구독
        playerItems.OnItemsChanged += UpdateInventoryUI;

        int idx = 0;
        foreach (Slot slot in slots)
        {
            slot.slotNum = idx;
            idx++;
        }

        // 인벤토리 UI 초기화
        UpdateInventoryUI();

        ClearSlots();
    }

    private void OnDisable()
    {
        // 인벤토리가 비활성화될 때 이벤트 구독 해제
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
