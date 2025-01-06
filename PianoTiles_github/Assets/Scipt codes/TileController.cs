using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TileController : MonoBehaviour
{
    public int line; // The line this tile belongs to (1 = V, 2 = B, etc.)
    public GameObject correctPressEffectPrefab; // The effect prefab for correct press
    public AudioSource hitSFX; // Sound effect for a hit
    public AudioSource missSFX; // Sound effect for a miss
    public TextMeshProUGUI scoreText; // UI text to display the score
    public Transform hitLine; // Reference to the hit line position

    private bool isHittable = false; // Whether the tile is within the hit area
    private bool hasBeenPressed = false; // To track if the tile has already been pressed
    private static int totalScore = 0; // Total score (static for persistence across instances)
    private float tileHeight; // Height of the tile

    // When loading a new scene, check if it's the Game Over or Main Menu and display/reset score accordingly.
    private void Start()
    {
        tileHeight = GetComponent<SpriteRenderer>().bounds.size.y; // Get the tile height based on its sprite

        // If we're in the GameOver scene, show the score.
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            DisplayScoreInGameOver();
        }
        else
        {
            // Reset score at the start of the game
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                totalScore = 0; // Reset score if starting a new game
            }

            UpdateScoreText(); // Ensure the score is displayed correctly on start
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
        else
        {
            Debug.LogError("Score Text is not assigned!");
        }
    }

    private void DisplayScoreInGameOver()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Final Score: {totalScore}";
        }
        else
        {
            Debug.LogError("Score Text is not assigned in the GameOver scene!");
        }
    }

    private int CalculateScore()
    {
        if (hitLine == null)
        {
            Debug.LogError("HitLine is not assigned!");
            return 0;
        }

        float distanceToHitLine = Mathf.Abs(transform.position.y - hitLine.position.y);

        // Calculate score based on the hit position
        if (distanceToHitLine <= tileHeight * 0.25f)
            return 10; // Perfect hit
        else if (distanceToHitLine <= tileHeight * 0.5f)
            return 5; // Good hit
        else if (distanceToHitLine <= tileHeight * 0.75f)
            return 2; // Okay hit
        else
            return 0; // Miss
    }

    private void Hit()
    {
        int points = CalculateScore();
        totalScore += points; // Add points to the static totalScore variable

        if (hitSFX != null)
            hitSFX.Play();

        UpdateScoreText();

        Debug.Log($"Tile hit! Points awarded: {points}");
    }

    private void Miss()
    {
        Debug.Log("Tile missed! 0 points awarded.");
        if (missSFX != null)
            missSFX.Play();
    }

    void Update()
    {
        if (isHittable && !hasBeenPressed)
        {
            bool keyMatched = false;

            // Check for keyboard input
            if (line == 1 && Input.GetKeyDown(KeyCode.V))
                keyMatched = true;
            else if (line == 2 && Input.GetKeyDown(KeyCode.B))
                keyMatched = true;
            else if (line == 3 && Input.GetKeyDown(KeyCode.N))
                keyMatched = true;
            else if (line == 4 && Input.GetKeyDown(KeyCode.M))
                keyMatched = true;

            // Check for Arduino input
            if (line == 1 && ArduinoInput.receivedData == "V")
                keyMatched = true;
            else if (line == 2 && ArduinoInput.receivedData == "B")
                keyMatched = true;
            else if (line == 3 && ArduinoInput.receivedData == "N")
                keyMatched = true;
            else if (line == 4 && ArduinoInput.receivedData == "M")
                keyMatched = true;

            if (keyMatched)
            {
                Hit();

                // Instantiate the particle effect at the tile's position
                GameObject effect = Instantiate(correctPressEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.22f);

                // Destroy the tile
                Destroy(gameObject);

                hasBeenPressed = true;

                // Clear the Arduino input to prevent double presses
                ArduinoInput.receivedData = "";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            isHittable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            isHittable = false;

            if (!hasBeenPressed)
            {
                Miss();
                Destroy(gameObject);
            }
        }
    }

    // Call this method to reset the score when returning to the main menu
    public void ResetScore()
    {
        totalScore = 0;  // Reset score to zero
        UpdateScoreText(); // Update the UI
    }
}
