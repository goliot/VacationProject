using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void OnClick()
    {
        //TODO : ��ư Ŭ�� ���� ������
        if (item == null) return;
        Debug.Log("Slot onpointerup");
    }
}
