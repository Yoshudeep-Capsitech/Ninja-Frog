using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartGame()
    {
        AudioManager.instance.PlayButtonClickSound();
        // Unpause the game
        Time.timeScale = 1f;

        AudioManager.instance.RestartThemeMusic();

        // Load the first level
        SceneManager.LoadScene(1); 
    }

    public void GoToMainMenu()
    {
        AudioManager.instance.PlayButtonClickSound();
        // Unpause the game
        Time.timeScale = 1f;

        AudioManager.instance.SwitchToMenuMusic();

        // Load the main menu
        SceneManager.LoadScene(0);
    }
}