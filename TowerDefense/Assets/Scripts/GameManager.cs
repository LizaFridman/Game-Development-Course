using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum GameStatus {
    Next, Play, GameOver, Win
};
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private Text lbl_totalMoney;
    [SerializeField]
    private Text lbl_currentWave;
    [SerializeField]
    private Text lbl_totalEscaped;
    [SerializeField]
    private Text lbl_playButton;
    [SerializeField]
    private Button btn_play;

    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private int totalEnemies = 3;
    [SerializeField]
    private int enemiesPerSpawn;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemiesToSpawn = 0;
    private GameStatus currentGameStatus = GameStatus.Play;

    private const float spawnDelay = 0.5f;

    public List<Enemy> EnemyList = new List<Enemy>();
    
    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            lbl_totalMoney.text = "" + totalMoney;
        }
    }

    public int TotalEscaped {
        get {
            return totalEscaped;
        }
        set
        {
            totalEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return totalKilled;
        }
        set {
            totalKilled = value;
        }
    }

    public int RoundEscaped
    {
        get
        {
            return roundEscaped;
        }

        set
        {
            roundEscaped = value;
        }
    }

    void Start()
    {
        btn_play.gameObject.SetActive(false);
        ShowMenu();
    }

    private void Update()
    {
        
    }

    private void ShowMenu()
    {
        switch (currentGameStatus) {
            case GameStatus.GameOver:
                lbl_playButton.text = "Play Again!"; 
                //Add game over sound
                break;
            case GameStatus.Next:
                lbl_playButton.text = "Next Wave";
                break;
            case GameStatus.Play:
                lbl_playButton.text = "Play";
                break;
            case GameStatus.Win:
                lbl_playButton.text = "Play";
                break;
        }
        btn_play.gameObject.SetActive(true);
    }

    public void UpdateGameState() {
        lbl_totalEscaped.text = "Escaped " + TotalEscaped + "/10";
        if (RoundEscaped + TotalKilled == totalEnemies) {
            SetGameState();
            ShowMenu();
        }
    }

    public void SetGameState() {
        if (totalEscaped >= 10)
        {
            currentGameStatus = GameStatus.GameOver;
        }
        else if (waveNumber == 0 && RoundEscaped + TotalKilled == 0)
        {
            currentGameStatus = GameStatus.Play;
        }
        else if (waveNumber >= totalWaves)
        {
            currentGameStatus = GameStatus.Win;
        }
        else {
            currentGameStatus = GameStatus.Next;
        }
    }

    public void PlayButtonPressed() {
        //Debug.Log("Play Button Pressed");
        switch (currentGameStatus) {
            case GameStatus.Next:
                waveNumber++;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                TotalMoney = 10;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameBuildSitesTag();
                lbl_totalMoney.text = "" + TotalMoney;
                lbl_totalEscaped.text = "Escaped " + TotalEscaped + "/10";
                break;
        }
        DestroyAllEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        lbl_currentWave.text = "Wave " + (waveNumber + 1);

        StartCoroutine(Spawn());

        btn_play.gameObject.SetActive(false);
    }

    IEnumerator Spawn() {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (var i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    int rand = (int)Random.Range(0,3);
                    var newEnemy = Instantiate(enemies[enemiesToSpawn]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    //enemiesOnScreen++;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy) {
        EnemyList.Add(enemy);

    }

    public void Unregister(Enemy enemy) {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies() {
        foreach (Enemy enemy in EnemyList) {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }

    public void AddMoney(int amount) {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        TotalMoney -= amount;
    }
}
