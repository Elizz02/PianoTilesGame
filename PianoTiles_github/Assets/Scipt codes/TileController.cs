using UnityEngine;

public class TileController : MonoBehaviour
{
    public int line; // The line this tile belongs to (1 = V, 2 = B, etc.)
    public GameObject correctPressEffectPrefab; // The effect prefab for correct press
    private bool isHittable = false;  // Whether the tile is within the hit area
    private bool hasBeenPressed = false; // To track if the tile has already been pressed
    private string currentKey = ""; // To store the key that should trigger the press

    void Update()
    {
        // Only respond if the tile is hittable and hasn't been pressed yet
        if (isHittable && !hasBeenPressed)
        {
            // Check if the received data matches the expected key for this tile's line
            if (ArduinoInput.receivedData == currentKey)
            {
                Debug.Log($"Tile hit on line {line}!");

                // Instantiate the particle effect at the tile's position
                GameObject effect = Instantiate(correctPressEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.22f); // Adjust the time based on your particle duration

                // Destroy the tile after hitting the correct line
                Destroy(gameObject);

                // Mark the tile as pressed so it won't get pressed again
                hasBeenPressed = true;

                // Clear the received data to avoid triggering other tiles
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

            // Set the current key for the tile based on its line
            if (line == 1)
                currentKey = "V";
            else if (line == 2)
                currentKey = "B";
            else if (line == 3)
                currentKey = "N";
            else if (line == 4)
                currentKey = "M";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            isHittable = false; // Tile leaves the hit area
            Debug.Log($"Tile {line} is no longer hittable");
        }
    }
}
