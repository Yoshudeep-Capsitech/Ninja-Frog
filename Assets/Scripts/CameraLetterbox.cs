using UnityEngine;

public class CameraLetterbox : MonoBehaviour
{
    // This is your target aspect ratio (16:9)
    public float targetAspect = 16.0f / 9.0f;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateLetterbox();
    }

    void UpdateLetterbox()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Rect rect = new Rect(0, 0, 1.0f, 1.0f);

        if (scaleHeight < 1.0f)
        {
            // Window is taller than target (add bars top/bottom)
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            // Window is wider than target (add bars left/right)
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cam.rect = rect;
    }
}