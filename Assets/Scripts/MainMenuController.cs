using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    // This function will be linked to the Play button
    public void StartGame()
    {
        AudioManager.instance.SwitchToLevelMusic();
        AudioManager.instance.PlayButtonClickSound();
        SceneManager.LoadScene(1); 
        
    }

    // This function will be linked to the Quit button
    public void QuitGame()
    {
        AudioManager.instance.PlayButtonClickSound();
        Debug.Log("QUITTING GAME!");
        Application.Quit();
    }
}