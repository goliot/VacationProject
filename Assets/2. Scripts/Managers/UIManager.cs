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
        uiSet = new HashSet<GameObject>(); // HashSet 초기화
    }

    public void Inventory()
    {
        GameObject inventoryPrefab = FindUI("InventoryUI");
        if (inventoryPrefab == null)
        {
            Debug.LogWarning("Inventory UI 프리팹을 찾을 수 없습니다.");
            return;
        }

        // 인스턴스화된 오브젝트가 uiSet에 있는지 확인
        /*foreach (GameObject ui in uiSet)
        {
            if (ui.name == inventoryPrefab.name + "(Clone)" && ui.activeSelf)
            {
                // UI가 이미 화면에 있는 경우
                return;
            }
        }*/

        // UI가 화면에 없으면 생성
        GameObject createdInventory = Instantiate(inventoryPrefab, canvas);
        uiStack.Push(createdInventory);
        uiSet.Add(createdInventory);
        createdInventory.GetComponent<InventoryUI>().UpdateInventoryUI(); // 인벤토리 초기화
    }

    public void CloseUI()
    {
        if (uiStack.Count > 0)
        {
            GameObject toCloseUI = uiStack.Pop();
            uiSet.Remove(toCloseUI); // UI를 제거할 때 uiSet에서 삭제
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
