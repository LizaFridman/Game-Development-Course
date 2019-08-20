using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool isPlayerInRange;
    private BoxCollider[] weaponColliders;

    // Start is called before the first frame update
    void Start()
    {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange = (Vector3.Distance(transform.position, player.transform.position) < range);
        //print("Player in range = " + isPlayerInRange);
    }

    IEnumerator Attack() {
        if (isPlayerInRange && !GameManager.instance.GameOver) {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(Attack());
    }

    public void EnemyBeginAttack() {
        foreach (var weapon in weaponColliders) {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack() {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }
}
