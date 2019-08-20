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
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
