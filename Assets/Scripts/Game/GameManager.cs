using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    internal const string NavLevel = "NAV LEVEL";
    internal const string LevelOne = "LEVEL ONE";
    internal const string LevelTwo = "LEVEL TWO";
    internal const string LevelThree = "LEVEL THREE";
    internal const string BossLevel = "BOSS LEVEL";
    internal const string VictoryLevel = "VICTORY_SCENE";

    internal const float CountTime = 30f;
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
    public bool isLevelOver = false;
    public bool isLevelLoading = false;

    public bool isBoss = false;
    internal float countdownTime = CountTime; // 60 seconds initially
    public Text countdownText;

    private const string QuestionsCSVPath = "Assets/Resources/questions.csv"; // Modify the path accordingly

    public List<QuestionData> questions;

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

            LoadQuestionsFromCSV();
            //printQuestions();
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event

        }
    }


    public void printQuestions()
    {
        if (questions != null && questions.Count > 0)
        {
            foreach (var question in questions)
            {
                Debug.Log($"Question: {question.question} \n" +
                    $"Difficulty: {question.difficulty}" +
                    $"Answers:" +
                    $"1: {question.answer1}" +
                    $"2: {question.answer2}" +
                    $"3: {question.answer3}" +
                    $"4: {question.answer4}" +
                    $"Correct Answer: {question.correctAnswer}" +
                    $"------------------------");
            }
        }
        else
        {
            Debug.LogError("Failed to load questions.");
        }
    }
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


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event when the GameManager is destroyed
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        if (player != null) player.ResetPlayer(isBoss);

        countdownTime = CountTime;
        isLevelOver = false;
        isLevelLoading = false;

        // Find the countdownText in the loaded scene

        GameObject timer;

        if(timer = GameObject.FindGameObjectWithTag("Timer"))
        {
            countdownText = timer.transform.GetChild(0).GetComponent<Text>();

        }

        Debug.Log(countdownText);

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

        /*
         * Nice little cheat code to be able to beat levels :)
         */
        if (Input.GetKeyDown(KeyCode.B)) {
            isLevelOver = true;
        }
    }

    internal void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownTime -= Time.deltaTime;
    }

    public void LoadLevel(Level level)
    {
        isLevelLoading = true;
        switch (level)
        {
            case Level.Nav:
                SceneManager.LoadScene(NavLevel);
                currentLevel = level;
                isBoss = false;
                break;
            case Level.One:
                SceneManager.LoadScene(LevelOne);
                currentLevel = level;
                isBoss = false;
                break;
            case Level.Two:
                SceneManager.LoadScene(LevelTwo);
                currentLevel = level;
                isBoss = false;
                break;
            case Level.Three:
                SceneManager.LoadScene(LevelThree);
                currentLevel = level;
                isBoss = false;
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

    public void LoadNextLevel()
    {
        switch (currentLevel)
        {
            case Level.One:
            case Level.Two:
                if (isBoss)
                {
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
                    LoadLevel(Level.Victory);
                }
                else
                {
                    LoadLevel(Level.Boss);
                }
                break;
        }
    }

    public void ResetLevel()
    {
        LoadLevel(currentLevel);
    }
}
