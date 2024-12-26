using UnityEngine;

public class TileController : MonoBehaviour
{
    public int line; // The line this tile belongs to (1 = V, 2 = B, etc.)
    public GameObject correctPressEffectPrefab; // The effect prefab for correct press
    private bool isHittable = false;  // Whether the tile is within the hit area
    private bool hasBeenPressed = false; // To track if the tile has already been pressed

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
        }
    }
}

