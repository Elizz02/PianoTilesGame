using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float fallSpeed = 2.0f; // Speed of the tile's fall
    private SpriteRenderer tileRenderer; // For managing opacity
    private bool hasCrossedLine = false; // To check if the tile has entered the line's area
    private float timeEnteredHitLine = 0f; // Tracks when the tile entered the line
    public float missTime = 0.8f; // Time after crossing the line when a tile becomes unpressable

    void Start()
    {
        // Get the SpriteRenderer for opacity changes
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move the tile downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // If the tile crossed the line and the missTime has elapsed
        if (hasCrossedLine && Time.time > timeEnteredHitLine + missTime)
        {
            // Reduce opacity to indicate a missed press
            tileRenderer.color = new Color(1f, 1f, 1f, 0.3f); // Make it semi-transparent
            hasCrossedLine = false; // Ensure this only happens once
        }

        // Destroy the tile if it falls below the screen
        if (transform.position.y < -5) // Adjust based on your layout
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            // Record the time when the tile enters the hittable area
            hasCrossedLine = true;
            timeEnteredHitLine = Time.time;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            // Reset when the tile exits the hit line area
            hasCrossedLine = false;
        }
    }
}


