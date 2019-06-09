using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
    
    private const float spawnDelay = 0.5f;

    public List<Enemy> EnemyList = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
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
}
