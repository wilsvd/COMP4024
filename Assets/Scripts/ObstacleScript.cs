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
    public Text questionText;

    public List<QuestionData> questions;
    int randomQuestionIndex;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;

    void DisplayAnswerOnButton(Button button, string answer)
    {
        // Set the text on the button
        Debug.Log("SETUP BUTTON LISTENER");
        button.GetComponentInChildren<Text>().text = answer;
   
        //button.onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplication
        //button.onClick.AddListener(() => HandleButtonClick(answer));
        Debug.Log(button.name);
    }

    void HandleButtonClick(string selectedAnswer)
    {
        // Get the current question
        Debug.Log("Clicked");
        QuestionData currentQuestion = questions[randomQuestionIndex]; // Assuming questions are always displayed randomly

        // Check if the selected answer is correct
        if (selectedAnswer == currentQuestion.correctAnswer)
        {
            Debug.Log("Correct answer chosen!");

            // Close the popup
            popupCanvas.enabled = false;

            // Continue with the game or perform other actions
            // Add your logic here...
        }
        else
        {
            Debug.Log("Incorrect answer chosen!");
        }
    }


    private void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Collider2D component attached to the GameObject
        obstacleCollider = GetComponent<Collider2D>();


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
        
        gameManager = FindObjectOfType<GameManager>();
        questions = gameManager.questions;
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

                    DisplayQuestion();
                    popupCanvas.enabled = true;
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
    void DisplayQuestion()
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
        QuestionData currentQuestion = questions[randomQuestionIndex];

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
