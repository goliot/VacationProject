using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    #region Singleton
    public static Inventory instance;
    #endregion
    // 아이템 목록을 저장할 리스트
    public List<string> items = new List<string>();
    public Slot[] slots;
    public Transform slotHolder;

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
        }
    }

    private void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    public void Init()
    {
        // TODO : 현재 플레이어가 가진 아이템들을 불러오기
    }

    // 아이템 추가 메서드
    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log(item + "이(가) 인벤토리에 추가되었습니다.");
    }

    // 아이템 제거 메서드
    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item + "이(가) 인벤토리에서 제거되었습니다.");
        }
        else
        {
            Debug.LogWarning(item + "은(는) 인벤토리에 없습니다.");
        }
    }
}
