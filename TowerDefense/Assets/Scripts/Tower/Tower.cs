
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenAttacks;
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private Projectile _projectile;
    private Enemy _enemyTarget = null;
    private float _attackCounter;
    private bool _isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDropTower();
        _attackCounter -= Time.deltaTime;
        SetEnemyTarget();
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            Attack();
        }
    }

    public void Attack() {
        _isAttacking = false;
        if (_enemyTarget != null)
        {
            var newProjectile = Instantiate(_projectile) as Projectile;
            newProjectile.transform.localPosition = transform.localPosition;

            StartCoroutine(LaunchProjectile(newProjectile));
        }
    }

    IEnumerator LaunchProjectile(Projectile projectile)
    {
        while (GetTargetDistance(_enemyTarget) > 0.2f && 
            projectile != null && 
            _enemyTarget != null) {

            var direction = _enemyTarget.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, _enemyTarget.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }

        if (projectile != null || _enemyTarget == null) {
            Destroy(projectile);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy) {
        if (thisEnemy == null) {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null) {
                return 0f;
            }
        }

        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private void SetEnemyTarget()
    { 
        if (_enemyTarget == null || _enemyTarget.IsDead)
        {
            var nearestEnemy = GetNearestEnemyInRange();

            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= _attackRadius)
            {
                _enemyTarget = nearestEnemy;
            }
        }
        else {
            if (_attackCounter <= 0)
            {
                _isAttacking = true;
                _attackCounter = _timeBetweenAttacks;
            }
            else {
                _isAttacking = false;
            }

            if (Vector2.Distance(transform.localPosition, _enemyTarget.transform.localPosition) > _attackRadius)
            {
                _enemyTarget = null;
            }
        }

        
    }

    private List<Enemy> GetEnemiesInRange() {
        var eneiesInRange = new List<Enemy>();
        var enemiesOnScreen = GameManager.Instance.EnemyList;

        foreach (Enemy enemy in enemiesOnScreen) {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= _attackRadius) {
                eneiesInRange.Add(enemy);
            }
        }

        return eneiesInRange;
    }

    private Enemy GetNearestEnemyInRange() {
        Enemy nearest = null;
        var smallestDistance = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemiesInRange()) {
            var distance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);

             if (distance < smallestDistance) {
                nearest = enemy;
                smallestDistance = distance;
            }
        }

        return nearest;
    }

    private void CheckDropTower()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.TowerButtonPressed = null;
        }
    }
}
