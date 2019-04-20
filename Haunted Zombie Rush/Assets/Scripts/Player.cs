using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private AudioClip sfxJump;
    private Animator animator;
    private Rigidbody rigidBody;
    private const int LeftClick = 0;
    private bool isJump = false;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(LeftClick)) {
            animator.Play("Jump");
            audioSource.PlayOneShot(sfxJump);
            rigidBody.useGravity = true;
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        if (isJump) {
            isJump = false;
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }
}
