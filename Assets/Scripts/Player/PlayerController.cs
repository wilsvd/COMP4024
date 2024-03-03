using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float deathYThreshold = -20f; // Adjust this value based on your level design

    public GameObject spawnPoint;

    private void Start()
    {
        RepawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's Y position is below the death threshold
        if (transform.position.y < deathYThreshold)
        {
            // Call a method to handle player death (e.g., RespawnPlayer)
            GameManager.Instance.ResetLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && GameManager.Instance.isLevelOver && !GameManager.Instance.isLevelLoading)
        {
            GameManager.Instance.LoadNextLevel();
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
}
