using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the score text UI
    public AudioSource hitSFX; // Sound effect for a successful hit
    public AudioSource missSFX; // Sound effect for a miss

    private int totalScore = 0; // Tracks the player's total score

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    /// <summary>
    /// Called when the player hits a tile correctly.
    /// </summary>
    /// <param name="points">The points awarded for this hit.</param>
    public void AddScore(int points)
    {
        totalScore += points;

        if (hitSFX != null)
        {
            hitSFX.Play(); // Play hit sound effect
        }

        UpdateScoreUI();
        Debug.Log($"Score added: {points}. Total Score: {totalScore}");
    }

    /// <summary>
    /// Called when the player misses a tile.
    /// </summary>
    public void Miss()
    {
        if (missSFX != null)
        {
            missSFX.Play(); // Play miss sound effect
        }

        Debug.Log("Tile missed. No points awarded.");
    }

    /// <summary>
    /// Updates the score display in the UI.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
        else
        {
            Debug.LogError("Score Text UI is not assigned!");
        }
    }
}
