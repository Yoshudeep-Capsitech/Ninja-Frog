using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public FallingPlatform platformToFall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && platformToFall != null)
        {
            platformToFall.StartFall();
            
            platformToFall = null; 
            gameObject.SetActive(false);
        }
    }
}