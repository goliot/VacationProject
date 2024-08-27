using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInputHandler : MonoBehaviour
{
    public delegate void InventoryAction();
    public static event InventoryAction OnClosePressed;
    public static event InventoryAction OnInventoryPressed;

    private void OnGUI()
    {
        if (SceneManager.GetActiveScene().name == "Lobby" || SceneManager.GetActiveScene().name == "Loading")
        {
            return;
        }

        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown && e.keyCode == KeyCode.I)
        {
            OnInventoryPressed?.Invoke();
        }
        if(e.isKey && e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            OnClosePressed?.Invoke();
        }
    }
}