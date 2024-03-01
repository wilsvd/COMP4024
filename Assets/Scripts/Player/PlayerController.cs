using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isLevelLoading = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLevelLoading && collision.CompareTag("Door"))
        {
            isLevelLoading = true;
            GameManager.Instance.LoadNextLevel();
        }
    }
}
