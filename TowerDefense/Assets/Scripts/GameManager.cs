using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;

    private int enemiesOnScreen = 0;
    private const float spawnDelay = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn() {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (var i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScreen < maxEnemiesOnScreen)
                {
                    int rand = (int)Random.Range(0,3);
                    var newEnemy = Instantiate(enemies[rand]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen++;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RemoveEnemyFromScreen() {
        if (enemiesOnScreen > 0) {
            enemiesOnScreen--;
        }
    }
}
