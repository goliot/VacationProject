using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_Equipments : Slot
{
    // TODO : 아이템 장착 해제에 따른 업데이트 및 아이템 효과 적용
    public ItemType itemType; // 해당 스롯에 적용될 아이템 타입
    /*public int slotNum;
    public Item item;
    public Image itemIcon;
    public int itemCount;
    public TextMeshProUGUI itemCountText;*/ //부모에 있는것들

    public override void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void OnClick()
    {
        //TODO : 버튼 클릭 로직 재정의
        if (item == null) return;
        Debug.Log("Slot onpointerup");
    }
}
