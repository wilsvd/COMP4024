using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public float deathYThreshold = -20f; // Adjust this value based on your level design

    public GameObject spawnPoint;

    void Awake()
    {
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

    public void RepawnPlayer()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        // Set the player's position to the spawn point position
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Spawn point not specified for player respawn.");
        }
    }
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
        if (collision.CompareTag("Door") && GameManager.Instance.isLevelOver && !GameManager.Instance.isLevelLoading)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }

}
