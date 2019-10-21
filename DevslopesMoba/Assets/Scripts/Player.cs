using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingDistance = 10f;
    private Transform targetedEnemy;
    private bool enemyClicked = false;
    private bool walking = false;
    private bool attacking = false;
    private Animator animator;
    private NavMeshAgent navAgent;
    private float nextFire;
    [SerializeField]
    private float timeBetweenShots = 1;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(bulletSpawnPoint);
        Assert.IsNotNull(bulletPrefab);
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire2")) {
            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.collider.CompareTag("Enemy"))
                {
                    targetedEnemy = hit.transform;
                    enemyClicked = true;
                    //walking = false;
                }
                else {
                    walking = true;
                    attacking = false;
                    enemyClicked = false;
                    navAgent.destination = hit.point;
                    navAgent.isStopped = false;
                }
            }
        }

        if (enemyClicked) {
            MoveAndShoot();
        }

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            walking = false;
        }
        else {
            if (!attacking)
            {
                walking = true;
            }
        }
        
        animator.SetBool("IsWalking", walking);
    }

    void MoveAndShoot() {
        if (targetedEnemy == null) {
            return;
        }

        navAgent.destination = targetedEnemy.position;

        if (navAgent.remainingDistance >= shootingDistance) {
            navAgent.isStopped = false;
            walking = true;
        }

        if (navAgent.remainingDistance <= shootingDistance) {
            transform.LookAt(targetedEnemy);

            if (Time.time > nextFire) {
                attacking = true;
                nextFire = Time.time + timeBetweenShots;
                Fire();
            }

            navAgent.isStopped = true;
            walking = false;
        }
    }

    void Fire()
    {
        animator.SetTrigger("Attack");
        var fireball = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as GameObject;
        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 4;
        Destroy(fireball, 3.5f);
    }
}