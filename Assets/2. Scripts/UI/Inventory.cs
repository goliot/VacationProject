using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : DraggableUI
{
    #region Singleton
    public static Inventory instance;
    #endregion
    // ������ ����� ������ ����Ʈ
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
        // TODO : ���� �÷��̾ ���� �����۵��� �ҷ�����
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
