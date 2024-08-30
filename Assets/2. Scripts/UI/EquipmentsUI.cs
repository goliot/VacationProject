using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsUI : DraggableUI
{
    public static EquipmentsUI instance;

    public PlayerItems playerItems;
    public Slot_Equipments[] slots;

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        playerItems = GameManager.Instance.player.GetComponent<PlayerItems>();
        playerItems.OnEquipmentItemsChanged += UpdateEquipmentUI;
    }

    private void OnDisable()
    {
        playerItems.OnEquipmentItemsChanged -= UpdateEquipmentUI;
    }

    public void UpdateEquipmentUI()
    {
        foreach(Slot_Equipments slot in slots)
        {
            if(playerItems.equipments.ContainsKey(slot.itemType))
            {
                slot.item = playerItems.equipments[slot.itemType];
                slot.UpdateSlotUI();
            }
            else
            {
                slot.item = null;
                slot.UpdateSlotUI();
            }
        }
    }
}
