using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinScript : MonoBehaviour
{
    [Tooltip("Check this box ONLY if this is the final level of the game.")]
    [SerializeField] private bool isLastLevel = false;
    private bool hasWon = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            StartCoroutine(PlayerWinSequence());
        }
    }

    IEnumerator PlayerWinSequence()
    {
        AudioManager.instance.PlayWinSound();

        yield return new WaitForSeconds(1.0f);

        if (isLastLevel)
        {
            SceneManager.LoadScene("EndScene"); 
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {

                SceneManager.LoadScene(0); 
            }
        }
    }
}