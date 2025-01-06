using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This is called when the player clicks the "Play Game" button
    public void PlayGame()
    {
        
        // Load the game scene (replace "SampleScene" with the correct scene name)
        SceneManager.LoadSceneAsync("SampleScene");
    }
}

