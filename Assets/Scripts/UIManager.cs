using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;

    void Start()
    {
        GameManager manager = GameManager.instance;

        if (manager != null)
        {
            livesText.text = "Lives: " + manager.currentLives.ToString();
        }
        else
        {
            Debug.LogWarning("GameManager not found in scene!");
            livesText.text = "Lives: ";
        }
        if (levelText != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            string formattedName = sceneName.Replace("_0", " ").Replace("_", " ");

            levelText.text = formattedName;
        }
    }
}