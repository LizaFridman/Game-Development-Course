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
    private int maxEnemiesOnScreen;
    [SerializeField]
    private int totalEnemies;
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

    void Start()
    {
        btn_play.gameObject.SetActive(false);
        ShowMenu();
    }

    private void Update()
    {
        //CheckDropTower();
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

    /*private void CheckDropTower() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.TowerButtonPressed = null;
        }
    }*/

    IEnumerator Spawn() {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (var i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    int rand = (int)Random.Range(0,3);
                    var newEnemy = Instantiate(enemies[rand]) as GameObject;
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
