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
        Player player = FindObjectOfType<Player>();
        if (player != null) player.ResetPlayer(isBoss);

        countdownTime = CountTime;
        isLevelOver = false;
        isLevelLoading = false;

        // Find the countdownText in the loaded scene
        countdownText = FindObjectOfType<Text>();
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
