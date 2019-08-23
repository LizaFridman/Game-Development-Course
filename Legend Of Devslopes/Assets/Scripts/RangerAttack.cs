using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private Transform fireLocation;
    private Animator anim;
    private GameObject player;
    private bool isPlayerInRange;
    private EnemyHealth enemyHealth;

    private GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        arrow = GameManager.instance.Arrow;

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange = (Vector3.Distance(transform.position, player.transform.position) < range &&
                           enemyHealth.IsAlive);
        if (isPlayerInRange)
        {
            RotateTowards(player.transform);
        }

        //print("Player in range = " + isPlayerInRange);
    }

    IEnumerator Attack()
    {
        if (isPlayerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(Attack());
    }

    private void RotateTowards(Transform player)
    {
        var direction = (player.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

    }

    public void FireArrow()
    {
        var newArrow = Instantiate(arrow);
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;

        newArrow.GetComponentInChildren<Rigidbody>().velocity = transform.forward * 25f;
    }
}