using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    // 0 rep
    public int currentLevel = 0;
    public bool isBoss = false;

    enum Level
    {
        Nav,
        One,
        Two,
        Three,
    }
    // Define a static property to access the instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // If no instance exists, find one in the scene
                instance = FindObjectOfType<GameManager>();

                // If still no instance exists, create a new one
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
        }
    }

    /* Loading from the navigation page
     *
     */
    public void LoadLevel(int level)
    {
        switch ((Level) level)
        {
            case Level.One:
                SceneManager.LoadScene("LEVEL ONE");
                currentLevel = 1;
                break;
            case Level.Two:
                SceneManager.LoadScene("LEVEL TWO");
                currentLevel = 2;
                break;
            case Level.Three:
                SceneManager.LoadScene("LEVEL THREE");
                currentLevel = 3;
                break;
        }
    }

    public void LoadNextLevel()
    {

        // Handle level transitions
        switch ((Level) currentLevel)
        {
            case Level.One:
                if (isBoss)
                {
                    SceneManager.LoadScene("LEVEL TWO");
                    isBoss = false;
                    currentLevel++;
                }
                else
                {
                    SceneManager.LoadScene("BOSS LEVEL");
                    isBoss = true;
                }
                break;

            case Level.Two:
                if (isBoss)
                {
                    SceneManager.LoadScene("LEVEL THREE");
                    isBoss = false;
                    currentLevel++;
                }
                else
                {
                    SceneManager.LoadScene("BOSS LEVEL");
                    isBoss = true;
                }
                break;

            case Level.Three:
                if (isBoss)
                {
                    SceneManager.LoadScene("NAV LEVEL");
                    isBoss = false;
                    currentLevel = (int)Level.Nav;
                }
                else
                {
                    SceneManager.LoadScene("BOSS LEVEL");
                    isBoss = true;
                }
                break;

            default:
                // Reset to the initial level if an unknown level is reached
                currentLevel = (int)Level.Nav;
                SceneManager.LoadScene("NAV LEVEL");
                isBoss = false;
                break;
        }
         // Increment the level
            
    }

}
