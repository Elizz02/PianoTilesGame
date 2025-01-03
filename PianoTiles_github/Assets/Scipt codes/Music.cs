using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;

    public AudioSource gameMusic;

    void Awake()
    {
        // Ensure only one MusicManager exists and it persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate MusicManager if one already exists
        }
    }
}
