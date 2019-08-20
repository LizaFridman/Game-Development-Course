using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

    void Awake() {
        Assert.IsNotNull(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Assert.IsNotNull(anim);
        nav = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(nav);
        enemyHealth = GetComponent<EnemyHealth>();
        Assert.IsNotNull(enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GameOver &&
            enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }
        else if (!enemyHealth.IsAlive)
        {
            nav.enabled = false;
        }
        else {
            nav.enabled = false;
            anim.Play("Idle");
        }
    }
}
