using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    private Animator animator;
    private Rigidbody rigidBody;
    private const int LeftClick = 0;

    private bool isJump = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(LeftClick)) {
            animator.Play("Jump");
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
