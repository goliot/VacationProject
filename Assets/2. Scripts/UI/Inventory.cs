using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    // ������ ����� ������ ����Ʈ
    public List<string> items = new List<string>();

    public void Init()
    {

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
