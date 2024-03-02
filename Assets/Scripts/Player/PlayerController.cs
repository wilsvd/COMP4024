using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isLevelLoading = false;
    public float deathYThreshold = -20f; // Adjust this value based on your level design

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
        if (!isLevelLoading && collision.CompareTag("Door") && GameManager.Instance.isLevelOver)
        {
            isLevelLoading = true;
            GameManager.Instance.LoadNextLevel();
        }
    }
}
