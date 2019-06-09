
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;

    private Projectile projectile;
    private Enemy enemyTarget = null;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Enemy> GetEnemiesInRange() {
        var eneiesInRange = new List<Enemy>();
        var enemiesOnScreen = GameManager.Instance.EnemyList;

        foreach (Enemy enemy in enemiesOnScreen) {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius) {
                eneiesInRange.Add(enemy);
            }
        }

        return eneiesInRange;
    }

    private Enemy GetNearestEnemy() {
        Enemy nearest = null;
        var smallestDistance = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemiesInRange()) {
            var distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < smallestDistance) {
                nearest = enemy;
                smallestDistance = distance;
            }
        }

        return nearest;
    }
}
