using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Player player;
    [SerializeField] private Rock[] rocks;

    private const string scorePrefix = "Total Coins: ";
    private Vector3 originalObsticalesPosition = new Vector3(11.97f, 8.8f, -3.830773f);

    private bool isPlayerActive = false;
    private bool isGameOver = false;
    private bool isGameStarted = false;
    private int coinsGathered = 0;

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
        Assert.IsNotNull(gameOverMenu);
        Assert.IsNotNull(player);
        Assert.IsNotNull(rocks);
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CoinGathered() {
        coinsGathered++;
    }

    public void PlayerCollided()
    {
        isGameOver = true;
        isPlayerActive = false;
        isGameStarted = false;

        UpdateCoinScore(gameOverMenu);

        gameOverMenu.SetActive(true);
    }

    private void UpdateCoinScore(GameObject menuObject) {
        var scoreText = menuObject.GetComponentInChildren<Text>();
        if (scoreText != null) {
            scoreText.text = scorePrefix + coinsGathered;
        }
    }

    public void PlayerStartedGame()
    {
        isPlayerActive = true;
    }

    public void ReplayGame() {
        ResetGame();
        EnterGame();
    }

    private void ResetGame()
    {
        isGameOver = false;
        isGameStarted = false;
        isPlayerActive = false;

        coinsGathered = 0;

        player.ResetPlayer();

        foreach (var rock in rocks) {
            rock.Reset();
        }
    }
    
    public void EnterGame() {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        
        isGameStarted = true;
    }

    public void ReturnToMainMenu() {
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
