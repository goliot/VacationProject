using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> uiList;
    public Transform canvas;

    private Stack<GameObject> uiStack;

    private void Awake()
    {
        uiStack = new Stack<GameObject>();
    }

    public void Inventory()
    {
        GameObject invenroty = FindUI("Inventory");
        GameObject createdInventory = Instantiate(invenroty, canvas);
        uiStack.Push(createdInventory);
    }

    public void CloseUI()
    {
        if (uiStack.Count > 0) {
            GameObject toCloseUI = uiStack.Pop();
            Destroy(toCloseUI);
        }
    }

    private GameObject FindUI(string name)
    {
        foreach(GameObject go in uiList)
        {
            if (go.name == name) return go;
        }
        return null;
    }
}
