using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GameObject player;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] powerUpSpawns;
    [SerializeField] private GameObject tanker;
    [SerializeField] private GameObject ranger;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject healthPowerUp;
    [SerializeField] private GameObject speedPowerUp;
    [SerializeField] Text levelText;
    [SerializeField] int maxPowerUps = 4;

    private bool isGameOver = false;
    private int currentLevel;
    private int powerUps = 0;

    private float generatedSpawnTime = 1;
    private float currentSpawnTime = 0;

    private float powerUpSpawnTime = 5;
    private float currentPowerUpSpawnTime = 0;

    private GameObject newEnemy;
    private GameObject newPowerUp;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void KillEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }

    public void RegisterPowerUp() {
        powerUps++;
    }

    public bool GameOver {
        get {
            return isGameOver;
        }
    }

    public GameObject Player {
        get {
            return player;
        }
    }

    public GameObject Arrow
    {
        get
        {
            return arrow;
        }
    }
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentLevel = 1;
        StartCoroutine(Spawn());
        StartCoroutine(PowerUpSpawn());
    }

    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        currentPowerUpSpawnTime += Time.deltaTime;
    }

    public void PlayerHit(int currentHp) {
        if(currentHp > 0)
        {
            isGameOver = false;
        }
        else {
            isGameOver = true;
        }
    }

    IEnumerator Spawn()
    {
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;

            if (enemies.Count < currentLevel)
            {
                var randomNumber = Random.Range(0, spawnPoints.Length - 1);
                var spawnLocation = spawnPoints[randomNumber];
                var randomEnemy = Random.Range(0, 3);
                if (randomEnemy == 0)
                {
                    newEnemy = Instantiate(soldier);
                }else if (randomEnemy == 1)
                {
                     newEnemy = Instantiate(ranger);

                }
                else
                {
                    newEnemy = Instantiate(tanker);
                }

                newEnemy.transform.position = spawnLocation.transform.position;
            }

            if (killedEnemies.Count >= currentLevel)
            {
                enemies.Clear();
                killedEnemies.Clear();

                yield return new WaitForSeconds(3f);
                currentLevel++;

                levelText.text = "Level " + currentLevel;
            }
        }

        yield return null;
        StartCoroutine(Spawn());
    }

    IEnumerator PowerUpSpawn() {
        // todo: spawn power ups where no power ups are located.
        if (currentPowerUpSpawnTime > powerUpSpawnTime)
        {
            currentPowerUpSpawnTime = 0;

            if (powerUps < maxPowerUps)
            {
                var randomNumber = Random.Range(0, powerUpSpawns.Length - 1);
                var spawnLocation = powerUpSpawns[randomNumber];

                var randomPowerUp = Random.Range(0,2);
                if (randomPowerUp == 0)
                {
                    newPowerUp = Instantiate(healthPowerUp) as GameObject;
                }
                else if(randomPowerUp == 1)
                {
                    newPowerUp = Instantiate(speedPowerUp) as GameObject;
                }

                newPowerUp.transform.position = spawnLocation.transform.position;
            }
        }

        yield return null;
        StartCoroutine(PowerUpSpawn());
    }
}
