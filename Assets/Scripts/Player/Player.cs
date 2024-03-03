using UnityEngine;

public class Player : MonoBehaviour
{
    // Other player-related variables and methods...

    private static Player instance;

    void Awake()
    {
        if (instance == null)
        {
            // If no instance exists, set the instance to this object
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }

    public void ResetPlayer(GameManager.Level level)
    {
        GetComponent<PlayerController>().RepawnPlayer();
        GetComponent<healthPoints>().InitialiseHealth();
        if (level != GameManager.Level.Boss) 
            transform.GetChild(0).GetComponent<Inventory>().ResetInventory();
    }

}
