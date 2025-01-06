using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This is called when the player clicks the "Play Game" button
    public void PlayGame()
    {
        // Reset the game data before loading the new scene
        ResetGame();

        // Load the game scene (replace "SampleScene" with the correct scene name)
        SceneManager.LoadSceneAsync("SampleScene");
    }

    // Reset game-related data (score, music, etc.)
    private void ResetGame()
    {
        // Example: Reset score if you have a ScoreManager
        TileController.instance.ResetScore();

        // Example: Reset player data if you have a PlayerManager
        // PlayerManager.instance.ResetPlayerData();

        Debug.Log("Game reset");
    }
}
