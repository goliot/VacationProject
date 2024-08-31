using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Equipments : Slot
{
    // TODO : ������ ���� ������ ���� ������Ʈ �� ������ ȿ�� ����
    public ItemType itemType; // �ش� ���Կ� ����� ������ Ÿ��
    /*public int slotNum;
    public Item item;
    public Image itemIcon;
    public int itemCount;
    public TextMeshProUGUI itemCountText;*/ //�θ� �ִ°͵�

    public override void UpdateSlotUI()
    {
        if (item != null)
        {
            itemIcon.gameObject.SetActive(true);
            itemIcon.sprite = item.itemImage;
        }
        else
        {
            RemoveSlot();
        }
    }

    public void OnClick()
    {
        //TODO : ��ư Ŭ�� ���� ������
        if (item == null) return;
        Debug.Log("Slot onpointerup");

        item.UnEquipItem();
        GameManager.Instance.player.GetComponent<PlayerItems>().UnequipItem(itemType);
    }
}
