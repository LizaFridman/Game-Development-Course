﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int statingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;

    private AudioSource audio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private int currentHealth;

    private ParticleSystem blood;

    public bool IsAlive {
        get {
            return isAlive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.RegisterEnemy(this);

        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();

        isAlive = true;
        currentHealth = statingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (dissapearEnemy) {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit &&
            !GameManager.instance.GameOver) {
            if (other.tag == "PlayerWeapon") {
                TakeHit();
                blood.Play();
                timer = 0f;
            }
        }
    }

    void TakeHit() {
        if (currentHealth > 0) {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
        }

        if (currentHealth <= 0) {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy() {
        GameManager.instance.KillEnemy(this);

        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;

        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy() {
        // wait 4 seconds after enemy dies
        yield return new WaitForSeconds(4f);

        // start to sink enemy
        dissapearEnemy = true;
        yield return new WaitForSeconds(2f);
        // after it sinks, destroy the object
        Destroy(gameObject);
    }
}
