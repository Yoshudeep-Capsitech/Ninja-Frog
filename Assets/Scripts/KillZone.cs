using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class KillZone : MonoBehaviour
{
    private bool isPlayerDead = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerDead)
        {
            isPlayerDead = true; 
            
            StartCoroutine(PlayerDeathSequence());
        }
    }

    IEnumerator PlayerDeathSequence()
    {
        AudioManager.instance.PlayFallDeadSound();

        yield return new WaitForSeconds(0.5f); 

        GameManager.instance.HandlePlayerDeath();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }
    }
}