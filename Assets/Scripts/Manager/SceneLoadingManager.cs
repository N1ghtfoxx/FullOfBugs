using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public static SceneLoadingManager Instance { get; private set; }

    private void Awake()
    {
        // if an instance already exists, destroy this duplicate and stop here
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // No instance yet - make this one the official instance
        Instance = this;
        // Keep this object alive when switching scenes
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
