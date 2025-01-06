using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public AudioSource musicSource; // Reference to the AudioSource playing the music
    public string nextSceneName = "GameOver"; // Name of the next scene to load
    public float delayAfterMusic = 2f; // Optional delay after music ends

    private bool hasSwitched = false; // Prevent multiple triggers

    private void Update()
    {
        if (musicSource != null && !musicSource.isPlaying && !hasSwitched)
        {
            hasSwitched = true; // Prevent multiple scene switches
            Debug.Log("Music stopped. Switching to the next scene...");
            Invoke(nameof(SwitchScene), delayAfterMusic); // Delay before switching
        }
    }

    private void SwitchScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next scene name is not set or empty!");
            return;
        }

        if (!SceneExists(nextSceneName))
        {
            Debug.LogError($"Scene '{nextSceneName}' is not in Build Settings! Please add it.");
            return;
        }

        Debug.Log($"Loading scene: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }

    private bool SceneExists(string sceneName)
    {
        // Check if the scene is added to Build Settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}