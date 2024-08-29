using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    public UIManager uiManager;
    public GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        UIInputHandler.OnInventoryPressed += OpenInventory;
        UIInputHandler.OnClosePressed += CloseUI;
    }

    private void OnDisable()
    {
        UIInputHandler.OnInventoryPressed -= OpenInventory;
        UIInputHandler.OnClosePressed -= CloseUI;
    }

    private void OpenInventory()
    {
        uiManager.Inventory();
    }

    private void CloseUI()
    {
        uiManager.CloseUI();
    }
}
