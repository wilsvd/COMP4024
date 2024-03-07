using UnityEngine;

// The PlayerManager class manages is a Singleton which handles the core player functionality.
public class PlayerManager : MonoBehaviour
{
    // Static instance of the PlayerManager to ensure only one instance exists
    private static PlayerManager instance;

    // Y position threshold to determine if the player has fallen below a death threshold.
    public float deathYThreshold = -20f;

    public GameObject spawnPoint;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Check if an instance already exists.
        if (instance == null)
        {
            // If no instance exists, set the instance to this object
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
        // Ensure the player is spawned upon initialization.
        RepawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's Y position is below the death threshold
        if (transform.position.y < deathYThreshold)
        {
            GameManager.Instance.ResetLevel();
        }
    }

    // Respawns the player at the specified spawn point.
    public void RepawnPlayer()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        // Set the player's position to the spawn point position if it exists.
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Spawn point not specified for player respawn.");
        }
    }

    // Resets the player's position, health, and inventory (optional) after a level reset.
    public void ResetPlayer(bool isBoss)
    {
        RepawnPlayer();
        GetComponent<PlayerHealth>().InitialiseHealth();
        if (!isBoss)
        {
            transform.GetChild(0).GetComponent<PlayerInventory>().ResetInventory();
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Load the next level if the player collided with a door, the level is over, and a level is not currently loading.
        if (collision.CompareTag("Door") && GameManager.Instance.isLevelOver && !GameManager.Instance.isLevelLoading)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }

}
