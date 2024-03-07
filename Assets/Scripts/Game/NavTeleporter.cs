using UnityEngine;
using UnityEngine.SceneManagement;

// The NavTeleporter class manages the teleporters on the navigation scene
public class NavTeleporter : MonoBehaviour
{
    // Assign a unique identifier to each sign in the Inspector
    public GameManager.Level levelIdentifier;
    private bool isTouching = false;

    private void Update()
    {
        // Check if the player is touching the teleporter and the Return key is pressed
        if (Input.GetKeyDown(KeyCode.Return) && isTouching)
        {
            // Load the level associated with the teleporter
            GameManager.Instance.LoadLevel(levelIdentifier);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
}
