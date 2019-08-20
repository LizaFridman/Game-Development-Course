using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] float timeSinceLastHitInSeconds = 2f;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        if (timer >= timeSinceLastHitInSeconds &&
            !GameManager.instance.GameOver) {

            if (other.tag == "Weapon") {
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit() {
        if (currentHealth > 0) {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
        }

        if (currentHealth <= 0) {
            KillPlayer();
        }
    }

    void KillPlayer() {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
    }
}
