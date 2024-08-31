using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentsUI : DraggableUI
{
    public static EquipmentsUI instance;

    public PlayerItems playerItems;
    public Slot_Equipments[] slots;

    [Header("# Texts")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI armorText;

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

    private void Update()
    {
        maxHpText.text = "Max HP : " + GameManager.Instance.player.GetComponent<PlayerController>().playerData.maxHealth.ToString();
        atkText.text = "Atk : " + GameManager.Instance.player.GetComponent<PlayerController>().playerData.attack + " + " +
            GameManager.Instance.player.GetComponent<PlayerController>().atkItemBonus;
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



    public void OnClickCloseBtn()
    {
        Destroy(gameObject);
    }
}
