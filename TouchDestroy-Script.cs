using UnityEngine;
using TMPro;

public class TouchDestroy : MonoBehaviour
{
    public GameObject enemyprefab;  // Reference to the enemy prefab (if needed)
    public float resetHeight = 10f;  // Reset height for game object
    public float fixedZPosition = -1f;  // Fixed z position for respawn

    public TMP_Text leftCounterText;  
    public TMP_Text rightCounterText;

    public TMP_Text leftResultText;
    public TMP_Text rightResultText;


    private static int leftDestroyCounter = 0;  // Counter for destroyed game objects on the left side
    private static int rightDestroyCounter = 0; // Counter for destroyed game objects on the right side

    // ref to timer script
    public Timer timerScript;

    void Start()
    {
        UpdateCounterText();  // Initialize counter text
    }

    void Update()
    {
        // check if there is a touch
        if (Input.touchCount > 0)
        {
            // iteration over all touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // check if touch of user began
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    // check if raycast is on gameobject
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        Debug.Log("Touch detected on the GameObject.");

                        // reference to the old GameObject
                        GameObject oldGameObject = gameObject;

                        // destroy gameobject if hit
                        Destroy(oldGameObject);

                        // Reactivate the old GameObject
                        Respawn(oldGameObject);

                        // Increment the counter when a game object is destroyed based on side
                        if (oldGameObject.transform.position.x < 0) // Left side
                        {
                            leftDestroyCounter++;
                            UpdateCounterText();
                        }
                        else // Right side
                        {
                            rightDestroyCounter++;
                            UpdateCounterText();
                        }
                    }
                }
            }
        }

        if (!timerScript.timerRunning)
        {
            if (leftDestroyCounter > rightDestroyCounter)
            {
                leftResultText.text = "Winner";
                rightResultText.text= "Loser";
                Debug.Log("Left side wins!");
            }
            else if (leftDestroyCounter < rightDestroyCounter)
            {
                Debug.Log("Right side wins!");
                leftResultText.text = "Loser";
                rightResultText.text= "Winner";
            }
            else
            {
                Debug.Log("It's a tie!");
                leftResultText.text = "Tie";
                rightResultText.text= "Tie";
            }
        }
    }

    void Respawn(GameObject oldGameObject)
    {
        Debug.Log("Respawn called");

        float randomX;
        Vector3 spawnPosition;

        // Check if the old GameObject was on the left or right side
        if (oldGameObject.transform.position.x < 0) // Left side
        {
            randomX = Random.Range(-8f, -2f);
            spawnPosition = new Vector3(randomX, resetHeight, fixedZPosition);
        }
        else // Right side
        {
            randomX = Random.Range(2f, 8f); 
            spawnPosition = new Vector3(randomX, resetHeight, fixedZPosition);
        }

        // Respawn old GameObject
        GameObject newSpawnedPrefab = Instantiate(oldGameObject, spawnPosition, Quaternion.identity);

        // Activate TouchDestroy and FallScript on new instance
        TouchDestroy touchDestroyScript = newSpawnedPrefab.GetComponent<TouchDestroy>();
        if (touchDestroyScript != null)
        {
            touchDestroyScript.enabled = true;
        }

        FallScript fallScript = newSpawnedPrefab.GetComponent<FallScript>();
        if (fallScript != null)
        {
            fallScript.enabled = true;
            fallScript.fallSpeed *= 1.05f;
        }

        Debug.Log("Old prefab reactivated successfully");
    }

    void UpdateCounterText()
    {
        leftCounterText.text = leftDestroyCounter.ToString();
        rightCounterText.text = rightDestroyCounter.ToString();

        Debug.Log("Left Counter: " + leftCounterText.text);
        Debug.Log("Right Counter: " + rightCounterText.text);
    }
}