using UnityEngine;

public class FallScript : MonoBehaviour
{
    public float fallSpeed = 0.2f;
    public float resetHeight = 10f;  // Reset height for game object
    public float fixedZPosition = -1f; // Fixed z position

    void Update()
    {
        // Move gameobject down
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // If at the bottom, reset to top
        if (transform.position.y < -resetHeight)
        {
            ResetToTopPosition();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, fixedZPosition);
    }

    void ResetToTopPosition()
    {
        // Reset to top position with a random x value
        float randomX;
        Vector3 spawnPosition;

        // Check if the falling object is on the left or right side
        if (transform.position.x < 0) // Left side
        {
            randomX = Random.Range(-7f, -3f); // Random X-position on the left side
            spawnPosition = new Vector3(randomX, resetHeight, fixedZPosition);
        }
        else // Right side
        {
            randomX = Random.Range(3f, 7f); // Random X-position on the right side
            spawnPosition = new Vector3(randomX, resetHeight, fixedZPosition);
        }

        transform.position = spawnPosition;
    }
}
