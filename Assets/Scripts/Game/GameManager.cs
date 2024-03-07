using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// The GameManager class manages the game state, including level loading, timers, and question data.
public class GameManager : MonoBehaviour
{
    // Constants defining scene names.
    internal const string NavLevel = "NAV LEVEL";
    internal const string LevelOne = "LEVEL ONE";
    internal const string LevelTwo = "LEVEL TWO";
    internal const string LevelThree = "LEVEL THREE";
    internal const string BossLevel = "BOSS LEVEL";
    internal const string VictoryLevel = "VICTORY_SCENE";

    // Constant defining the initial countdown time.
    internal const float CountTime = 30f;

    // Enumeration representing different game levels.
    public enum Level
    {
        Nav,
        One,
        Two,
        Three,
        Boss,
        Victory
    }
    public Level currentLevel = Level.Nav;
    private static GameManager instance;

    // Flags indicating the state of the game.
    public bool isLevelOver = false;
    public bool isLevelLoading = false;
    public bool isBoss = false;

    // Countdown timer variables.
    internal float countdownTime = CountTime; // 60 seconds initially
    public Text countdownText;

    // Path to the CSV file containing question data.
    private const string QuestionsCSVPath = "Assets/Resources/questions.csv";

    // List to store question data.
    public List<QuestionData> questions;

    // Variables to track the number of questions in the game and the current level.
    public int gameQuestionCount = 0;
    public int levelQuestionCount = 0;

    // Reference to the CanvasPopup.
    public Canvas CanvasPopup;


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

    // Singleton pattern to get the GameManager instance.
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Load questions from the CSV file.
            LoadQuestionsFromCSV();
            // Subscribe to the sceneLoaded event.
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
    }

    // Load questions from the CSV file and populate the questions list.
    private void LoadQuestionsFromCSV()
    {
        questions = new List<QuestionData>();

        try
        {
            using (StreamReader reader = new StreamReader(QuestionsCSVPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    QuestionData questionData = new QuestionData();
                    questionData.question = fields[0];
                    questionData.difficulty = fields[1];
                    questionData.answer1 = fields[2];
                    questionData.answer2 = fields[3];
                    questionData.answer3 = fields[4];
                    questionData.answer4 = fields[5];
                    questionData.correctAnswer = fields[6];

                    questions.Add(questionData);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading questions CSV: " + e.Message);
        }
    }

    // Unsubscribe from the sceneLoaded event when the GameManager is destroyed.
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event when the GameManager is destroyed
    }

    // Handle actions when a scene is loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        // Reset player, timers, and other variables when a new scene is loaded.
        if (player != null) player.ResetPlayer(isBoss);

        countdownTime = CountTime;
        isLevelOver = false;
        isLevelLoading = false;


        // Find the countdownText in the loaded scene
        if (currentLevel == Level.One || currentLevel == Level.Two || currentLevel == Level.Three)
        {
            GameObject timer = GameObject.FindGameObjectWithTag("Timer");
            if (timer)
            {
                countdownText = timer.transform.GetChild(0).GetComponent<Text>();
            }
        }
        else if (currentLevel == Level.Victory)
        {
            GameObject score = GameObject.FindGameObjectWithTag("ScoreText");
            if (score)
            {
                score.GetComponent<Text>().text = string.Format("{0}", gameQuestionCount);
            }
        }
        
    }

    private void Update()
    {
        // Update the countdown timer if the game is running and not in boss or home scene
        if (countdownText != null && countdownTime > 0 && !isBoss && currentLevel != (int)Level.Nav && !isLevelOver)
        {
            UpdateTimer();
            
        }
        else if (countdownTime <= 0 && !isLevelOver)
        {
            Debug.Log("Time's up!");
            isLevelOver = true;
        }

        if (currentLevel == Level.Victory && Input.GetKeyDown(KeyCode.Return))
        {
            LoadLevel(Level.Nav);
        }

        
         // Nice little cheat code to be able to open the doors on each level :)
        if (Input.GetKeyDown(KeyCode.B)) {
            isLevelOver = true;
        }
    }

    // Update the countdown timer text.
    internal void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownTime -= Time.deltaTime;
    }

    // Load a specific level based on the provided level enum.
    public void LoadLevel(Level level)
    {
        isLevelLoading = true;
        Debug.Log("levelQuestionCount: " + levelQuestionCount + " --- gameQuestionCount: " + gameQuestionCount);
        switch (level)
        {
            case Level.Nav:
                SceneManager.LoadScene(NavLevel);
                currentLevel = level;
                isBoss = false;
                levelQuestionCount = 0;
                break;
            case Level.One:
                SceneManager.LoadScene(LevelOne);
                currentLevel = level;
                isBoss = false;
                levelQuestionCount = 0;
                break;
            case Level.Two:
                SceneManager.LoadScene(LevelTwo);
                currentLevel = level;
                isBoss = false;
                levelQuestionCount = 0;
                break;
            case Level.Three:
                SceneManager.LoadScene(LevelThree);
                currentLevel = level;
                isBoss = false;
                levelQuestionCount = 0;
                break;
            case Level.Boss:
                SceneManager.LoadScene(BossLevel);
                isBoss = true;
                break;
            case Level.Victory:
                SceneManager.LoadScene(VictoryLevel);
                isBoss = false;
                currentLevel = Level.Victory;
                break;
        }
        
    }

    // Load the next level based on the current level.
    public void LoadNextLevel()
    {
        switch (currentLevel)
        {
            case Level.One:
            case Level.Two:
                if (isBoss)
                {
                    gameQuestionCount += levelQuestionCount;
                    LoadLevel(currentLevel+1);
                }
                else
                {
                    LoadLevel(Level.Boss);                    
                }
                break;
            case Level.Three:
                if (isBoss)
                {
                    gameQuestionCount += levelQuestionCount;
                    LoadLevel(Level.Victory);
                }
                else
                {
                    LoadLevel(Level.Boss);
                }
                break;
        }
    }

    // Reset the current level.
    public void ResetLevel()
    {
        LoadLevel(currentLevel);
    }
}
