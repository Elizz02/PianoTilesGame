using UnityEngine;
using TMPro;

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
    private int totalScore = 0; // Total score
    private float tileHeight; // Height of the tile

    private void Start()
    {
        tileHeight = GetComponent<SpriteRenderer>().bounds.size.y; // Get the tile height based on its sprite
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

    private int CalculateScore()
    {
        if (hitLine == null)
        {
            Debug.LogError("HitLine is not assigned!");
            return 0;
        }

        float distanceToHitLine = Mathf.Abs(transform.position.y - hitLine.position.y);

        // Calculate score based on the hit position
        if (distanceToHitLine <= tileHeight * 0.25f) // Bottom edge (25% of tile height)
            return 100;
        else if (distanceToHitLine <= tileHeight * 0.5f) // Halfway point (50% of tile height)
            return 50;
        else if (distanceToHitLine <= tileHeight * 0.75f) // Third quarter (75% of tile height)
            return 20;
        else
            return 0; // Missed or too far from the hit line
    }

    private void Hit()
    {
        int points = CalculateScore();
        totalScore += points;

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

        UpdateScoreText();
    }

    void Update()
    {
        // Only respond if the tile is hittable and hasn't been pressed yet
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

            // If either keyboard or Arduino input matched
            if (keyMatched)
            {
                Debug.Log($"Tile hit on line {line}!");

                // Call the Hit method
                Hit();

                // Instantiate the particle effect at the tile's position
                GameObject effect = Instantiate(correctPressEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.22f); // Adjust the time based on your particle duration

                // Destroy the tile after hitting the correct line
                Destroy(gameObject);

                // Mark the tile as pressed so it won't get pressed again
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
            isHittable = true; // Tile is within the hit area
            Debug.Log($"Tile {line} is hittable");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            isHittable = false; // Tile leaves the hit area
            Debug.Log($"Tile {line} is no longer hittable");

            // Call the Miss method when the tile exits the hit area without being pressed
            if (!hasBeenPressed)
            {
                Miss();
                Destroy(gameObject); // Destroy the tile after a miss
            }
        }
    }
}
