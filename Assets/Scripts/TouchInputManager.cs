using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public static TouchInputManager instance;

    public float HorizontalInput { get; private set; }
    
    private bool jumpWasPressed = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnLeftButtonDown()
    {
        HorizontalInput = -1f;
    }

    public void OnRightButtonDown()
    {
        HorizontalInput = 1f;
    }

    public void OnMoveButtonUp()
    {
        HorizontalInput = 0f;
    }

    public void OnJumpButtonDown()
    {
        jumpWasPressed = true;
    }


    public bool GetJumpInput()
    {
        if (jumpWasPressed)
        {
            jumpWasPressed = false; 
            return true;
        }
        return false;
    }
}