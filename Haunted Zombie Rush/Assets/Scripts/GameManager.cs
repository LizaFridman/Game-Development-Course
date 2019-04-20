using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private GameObject mainMenu;

    private bool isPlayerActive = false;
    private bool isGameOver = false;
    private bool isGameStarted = false;

    public bool IsPlayerActive
    {
        get { return isPlayerActive; }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public bool IsGameStarted
    {
        get { return isGameStarted; }
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
        Assert.IsNotNull(mainMenu);
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

    public void EnterGame() {
        mainMenu.SetActive(false);
        isGameStarted = true;
    }
}
