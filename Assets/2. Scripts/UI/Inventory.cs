using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    // 아이템 목록을 저장할 리스트
    public List<string> items = new List<string>();

    public void Init()
    {

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
