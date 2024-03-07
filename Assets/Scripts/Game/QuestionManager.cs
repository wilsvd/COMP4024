using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject swordPrefab; // Serialize the field for inspection in the Unity Editor
    [SerializeField]
    private GameObject bowPrefab;   // Serialize the field for a bow prefab
    [SerializeField]
    private Sprite blankSprite;      // Serialize the field for a blank sprite

    private bool hasCollided = false;
    private SpriteRenderer spriteRenderer;
    private bool isEmpty = false;
    public Canvas popupCanvas;
    public Text questionText;
    private Collider2D obstacleCollider;


    public List<GameManager.QuestionData> questions;
    int randomQuestionIndex;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;
    public PlayerMovement playerMovement;
    internal void DisplayAnswerOnButton(Button button, string answer)
    {
        button.GetComponentInChildren<Text>().text = answer;

        button.onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplication
        button.onClick.AddListener(() => HandleButtonClick(answer));
        Debug.Log(button.name);

        Debug.Log(button.isActiveAndEnabled);
    }

    internal void HandleButtonClick(string selectedAnswer)
    {
        // Get the current question
        GameManager.QuestionData currentQuestion = questions[randomQuestionIndex]; // Assuming questions are always displayed randomly

        // Check if the selected answer is correct
        if (selectedAnswer == currentQuestion.correctAnswer)
        {
            Debug.Log("Correct answer chosen!");

            // Determine the item to spawn based on random chance
            SpawnRandomItem();
            // Continue with the game or perform other actions
            // Add your logic here...
        }
        else
        {
            Debug.Log("Incorrect answer chosen!");
        }
        // Set the sprite to a blank square with the same size as the original box
        spriteRenderer.sprite = blankSprite;
        spriteRenderer.size = GetComponent<SpriteRenderer>().size;
        popupCanvas.enabled = false;
        playerMovement.canMove = true;
        isEmpty = true;

    }


    private void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Collider2D component attached to the GameObject
        obstacleCollider = GetComponent<Collider2D>();

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        // Find the canvas GameObject by name
        GameObject canvasObject = GameObject.Find("CanvasPopup");

        if (canvasObject != null)
        {
            // Get the Canvas component from the GameObject
            popupCanvas = canvasObject.GetComponent<Canvas>();

            if (popupCanvas != null)
            {
                // Find the Text component within the popupCanvas
                popupCanvas.enabled = false;
            }
            else
            {
                Debug.LogError("Canvas component not found on the GameObject named");
            }
        }
        else
        {
            Debug.LogError("GameObject with the name not found.");
        }
        
        questions = GameManager.Instance.questions;
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
                    playerMovement.canMove = false;
                    DisplayQuestion();
                    popupCanvas.enabled = true;
                    // NEED TO SET THE TEXT IN HERE FOR questionText

                }

                // Set hasCollided to true to prevent further collisions
                hasCollided = true;
            }
        }
    }
    internal void DisplayQuestion()
    {
        // Check if there are any questions available
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("No questions available.");
            return;
        }

        // Pick a random question index
        randomQuestionIndex = Random.Range(0, questions.Count);

        // Get the randomly selected question
        GameManager.QuestionData currentQuestion = questions[randomQuestionIndex];

        // Display question text
        questionText.text = currentQuestion.question;

        // Display multiple-choice answers on buttons
        DisplayAnswerOnButton(answerButton1, currentQuestion.answer1);
        DisplayAnswerOnButton(answerButton2, currentQuestion.answer2);
        DisplayAnswerOnButton(answerButton3, currentQuestion.answer3);
        DisplayAnswerOnButton(answerButton4, currentQuestion.answer4);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset hasCollided when the "Player" exits the collision with the "Obstacle"
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
        }
    }

    internal void SpawnRandomItem()
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
