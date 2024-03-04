using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject swordPrefab; // Serialize the field for inspection in the Unity Editor
    [SerializeField]
    private GameObject bowPrefab;   // Serialize the field for a bow prefab
    private bool hasCollided = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D obstacleCollider;

    private void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Collider2D component attached to the GameObject
        obstacleCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the "Player" and the collision hasn't been processed yet
        if (!hasCollided && collision.gameObject.CompareTag("Player"))
        {
            // Get the contact points of the collision
            ContactPoint2D[] contacts = collision.contacts;

            // Check if any contact point has a normal pointing downwards
            bool bottomCollisionDetected = System.Array.Exists(contacts, contact => contact.normal.y > 0.9f);

            if (bottomCollisionDetected)
            {
                Debug.Log("Player collided with Obstacle's bottom!");

                // Determine the item to spawn based on random chance
                SpawnRandomItem();

                // Disable the sprite renderer
                //spriteRenderer.enabled = false;

                // Disable the collider
                //obstacleCollider.enabled = false;

                // Set hasCollided to true to prevent further collisions
                hasCollided = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset hasCollided when the "Player" exits the collision with the "Obstacle"
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
        }
    }

    private void SpawnRandomItem()
    {
        // Generate a random value between 0 and 1
        float randomValue = Random.value;

        if (randomValue <= 0.2f) // 20% chance for a bow
        {
            Instantiate(bowPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        }
        else if (randomValue <= 0.4f) // 20% chance for a sword
        {
            Instantiate(swordPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        }
        else // 60% chance to print "hello"
        {
            Debug.Log("hello");
        }
    }
}