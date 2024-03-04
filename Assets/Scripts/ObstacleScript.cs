using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject swordPrefab; // Serialize the field for inspection in the Unity Editor
    [SerializeField]
    private GameObject bowPrefab;   // Serialize the field for a bow prefab
    [SerializeField]
    private Sprite blankSprite;      // Serialize the field for a blank sprite

    private bool hasCollided = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D obstacleCollider;
    private bool isEmpty = false;

    public Canvas popupCanvas;
    private GameManager gameManager;


    public List<QuestionData> questions;

    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string difficulty;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string correctAnswer;
    }


    private void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Collider2D component attached to the GameObject
        obstacleCollider = GetComponent<Collider2D>();

        popupCanvas.enabled = false;
        gameManager = FindObjectOfType<GameManager>();



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

                if (!isEmpty)
                {

                 // NEED TO SET THE TEXT IN HERE FOR questionText

                }


                // Determine the item to spawn based on random chance
                SpawnRandomItem();

                // Set the sprite to a blank square with the same size as the original box
                spriteRenderer.sprite = blankSprite;
                spriteRenderer.size = GetComponent<SpriteRenderer>().size;


                // Set hasCollided to true to prevent further collisions
                hasCollided = true;
                isEmpty = true;
            }
        }
    }
    void DisplayQuestion(int questionIndex)
    {
        // Ensure the question index is within bounds
        if (questionIndex < 0 || questionIndex >= questions.Count)
        {
            Debug.LogError("Invalid question index.");
            return;
        }
        // Get the current question
        QuestionData currentQuestion = questions[questionIndex];

        // Display question text
        questionText.text = currentQuestion.question;
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
        if(isEmpty == true)
        {
            return;
        }
        if (randomValue <= 0.5f) // 20% chance for a bow
        {
            Instantiate(bowPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        }
        else if (randomValue <= 1f) // 20% chance for a sword
        {
            Instantiate(swordPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        }
        // No need for an 'else' here since we don't want to do anything for the 60% chance to print "hello"
    }
}
