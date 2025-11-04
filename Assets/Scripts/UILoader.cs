using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    [Tooltip("Drag your Canvas Prefab here")]
    public GameObject uiCanvasPrefab;
    
    private GameObject currentCanvasInstance;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        CheckScene(SceneManager.GetActiveScene()); 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckScene(scene);
    }

    void CheckScene(Scene scene)
    {
        bool isMenuScene = scene.name == "MainMenu" || 
                           scene.name == "GameOverScene" || 
                           scene.name == "EndScene";

        if (isMenuScene)
        {
            if (currentCanvasInstance != null)
            {
                Destroy(currentCanvasInstance);
            }
        }
        else
        {
            if (currentCanvasInstance == null)
            {
                currentCanvasInstance = Instantiate(uiCanvasPrefab);
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}