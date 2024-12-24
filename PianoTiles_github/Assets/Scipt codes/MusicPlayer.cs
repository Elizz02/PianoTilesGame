using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource.Play(); // Play the music at the start
    }

    public float GetMusicTime()
    {
        return audioSource.time; // Get the current playback time
    }
}

