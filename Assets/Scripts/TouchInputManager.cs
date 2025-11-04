using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    [Header("Button Hold States")]
    public bool moveLeft;
    public bool moveRight;
    public bool jumpPressed;

    public void SetMoveLeft(bool state) => moveLeft = state;
    public void SetMoveRight(bool state) => moveRight = state;
    public void SetJump(bool state) => jumpPressed = state;

    void LateUpdate()
    {
        // Reset jump each frame so it only triggers once
        jumpPressed = false;
    }
}
