using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalLives = 5;
    
    [HideInInspector]
    public int currentLives;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            currentLives = totalLives;
        }
        else
        {

            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMainMenu();
            }
        }
    }

    // This function will be called by the PlayerController when it dies
    public void HandlePlayerDeath()
    {
        Debug.LogWarning("--- HANDLE PLAYER DEATH IS RUNNING! ---");
        
        // Subtract a life
        currentLives--;

        if (currentLives <= 0)
        {
            Debug.Log("GAME OVER! Loading Game Over Screen.");

            currentLives = totalLives; 

            AudioManager.instance.StopThemeMusic();

            AudioManager.instance.PlayGameOverSound();

            Time.timeScale = 0f;

            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive); 
        }
        else
        {
            Debug.Log("Player died. Lives left: " + currentLives);

            AudioManager.instance.RestartThemeMusic();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GoToMainMenu()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonClickSound();
        }

        // Unpause the game
        Time.timeScale = 1f;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.SwitchToMenuMusic();
        }
        
        // Load the main menu (build index 0)
        SceneManager.LoadScene(0);
    }
    public void ResetLives()
    {
        currentLives = totalLives;
    }
}