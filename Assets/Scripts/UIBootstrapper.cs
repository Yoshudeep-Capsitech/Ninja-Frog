using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBootstrapper : MonoBehaviour
{
    public GameObject mobileControlsPrefab;
    private static GameObject controlsInstance;
    private static bool initialized = false;

    void Awake()
    {
        if (!initialized)
        {
            controlsInstance = Instantiate(mobileControlsPrefab);
            DontDestroyOnLoad(controlsInstance);
            initialized = true;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level"))
        {
            controlsInstance.SetActive(true);
        }
        else
        {
            controlsInstance.SetActive(false);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
