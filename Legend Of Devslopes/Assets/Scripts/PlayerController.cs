﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private BoxCollider[] swordColliders;
    private GameObject fireTrail;
    private ParticleSystem fireTrailParticles;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        fireTrail = GameObject.FindWithTag("Fire") as GameObject;
        fireTrail.SetActive(false);
        
    }

    public void SpeedPowerUp()
    {
        StartCoroutine(FireTrailRoutine());
    }

    public IEnumerator FireTrailRoutine()
    {
        fireTrail.SetActive(true);
        moveSpeed = 10f;
        yield return new WaitForSeconds(10f);

        moveSpeed = 6f;

        fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();

        var emission = fireTrailParticles.emission;
        emission.enabled = false;
        yield return new WaitForSeconds(3);// wait until last emission is finished (1-2)
        emission.enabled = true;

        fireTrail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GameOver)
        {
            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(moveDirection * moveSpeed);

            if (moveDirection == Vector3.zero)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }

            if (Input.GetMouseButtonDown(0))
            { // Left Button
                anim.Play("DoubleChop");
            }

            if (Input.GetMouseButtonDown(1))
            {// Right Button
                anim.Play("SpinAttack");
            }
        }
    }

    void FixedUpdate() {
        if (!GameManager.instance.GameOver)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

            if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != currentLookTarget)
                {
                    currentLookTarget = hit.point;
                }

                // character rotation
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

            }
        }
    }

    public void BeginAttack() {
        foreach (var weapon in swordColliders) {
            weapon.enabled = true;
        }
    }

    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
        }
    }
}
