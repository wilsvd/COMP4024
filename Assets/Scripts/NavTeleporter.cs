using UnityEngine;
using UnityEngine.SceneManagement;

public class NavTeleporter : MonoBehaviour
{
    // Assign a unique identifier to each sign in the Inspector
    public int levelIdentifier;
    private bool isTouching = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isTouching)
        {   
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
