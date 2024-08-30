using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> uiList;
    public Transform canvas;

    private Stack<GameObject> uiStack;
    private HashSet<GameObject> uiSet;

    private void Awake()
    {
        uiStack = new Stack<GameObject>();
        uiSet = new HashSet<GameObject>(); // HashSet �ʱ�ȭ
    }

    public void Inventory()
    {
        GameObject inventoryPrefab = FindUI("InventoryUI");
        if (inventoryPrefab == null)
        {
            Debug.LogWarning("Inventory UI �������� ã�� �� �����ϴ�.");
            return;
        }

        // �ν��Ͻ�ȭ�� ������Ʈ�� uiSet�� �ִ��� Ȯ��
        /*foreach (GameObject ui in uiSet)
        {
            if (ui.name == inventoryPrefab.name + "(Clone)" && ui.activeSelf)
            {
                // UI�� �̹� ȭ�鿡 �ִ� ���
                return;
            }
        }*/

        // UI�� ȭ�鿡 ������ ����
        GameObject createdInventory = Instantiate(inventoryPrefab, canvas);
        uiStack.Push(createdInventory);
        uiSet.Add(createdInventory);
        createdInventory.GetComponent<InventoryUI>().UpdateInventoryUI(); // �κ��丮 �ʱ�ȭ
    }

    public void CloseUI()
    {
        if (uiStack.Count > 0)
        {
            GameObject toCloseUI = uiStack.Pop();
            uiSet.Remove(toCloseUI); // UI�� ������ �� uiSet���� ����
            Destroy(toCloseUI);
        }
    }

    private GameObject FindUI(string name)
    {
        foreach (GameObject go in uiList)
        {
            if (go.name == name) return go;
        }
        return null;
    }
}
