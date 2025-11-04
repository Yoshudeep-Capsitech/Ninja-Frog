using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonClickSound();
            AudioManager.instance.SwitchToMenuMusic();
        }
        SceneManager.LoadScene(0);
    }
}