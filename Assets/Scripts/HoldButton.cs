using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Jump }
    public ButtonType buttonType;

    private TouchInputManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<TouchInputManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (manager == null) return;
        switch (buttonType)
        {
            case ButtonType.Left: manager.SetMoveLeft(true); break;
            case ButtonType.Right: manager.SetMoveRight(true); break;
            case ButtonType.Jump: manager.SetJump(true); break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (manager == null) return;
        switch (buttonType)
        {
            case ButtonType.Left: manager.SetMoveLeft(false); break;
            case ButtonType.Right: manager.SetMoveRight(false); break;
            case ButtonType.Jump: manager.SetJump(false); break;
        }
    }
}
