using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private bool isPlayerActive = false;
    private bool isGameOver = false;

    public bool IsPlayerActive
    {
        get { return isPlayerActive; }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);// When switching between scenes, the game objects won't be destroyed
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerCollided()
    {
        isGameOver = true;
    }

    public void PlayerStartedGame()
    {
        isPlayerActive = true;
    }
}
