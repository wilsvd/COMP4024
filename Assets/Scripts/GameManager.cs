using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string NavLevel = "NAV LEVEL";
    private const string LevelOne = "LEVEL ONE";
    private const string LevelTwo = "LEVEL TWO";
    private const string LevelThree = "LEVEL THREE";
    private const string BossLevel = "BOSS LEVEL";

    private const float CountTime = 30f;
    public enum Level
    {
        Nav,
        One,
        Two,
        Three,
        Boss,
    }
    public Level currentLevel = Level.Nav;

    private static GameManager instance;
    public bool isLevelOver = false;
    public bool isBoss = false;
    private float countdownTime = CountTime; // 60 seconds initially
    public Text countdownText;


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

            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event when the GameManager is destroyed
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the countdownText in the loaded scene
        countdownText = FindObjectOfType<Text>();
    }

    private void Update()
    {
        // Update the countdown timer if the game is running and not in boss or home scene
        if (countdownText != null && countdownTime > 0 && !isBoss && currentLevel != (int)Level.Nav && !isLevelOver)
        {
            UpdateTimer();
            countdownTime -= Time.deltaTime;
        }
        else if (countdownTime <= 0 && !isLevelOver)
        {
            Debug.Log("Time's up!");
            isLevelOver = true;
        }

        /*
         * On the Boss Scene there isn't any timer so the level is over when the boss died.
         * I'm adding an input to mock Boss Dying to see if player can go through the door
         */
        if (Input.GetKeyDown(KeyCode.B)) {
            isLevelOver = true;
        }
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LoadLevel(Level level)
    {
        countdownTime = CountTime;
        isLevelOver = false;

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
                    LoadLevel(Level.Nav);
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
