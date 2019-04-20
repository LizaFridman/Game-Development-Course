using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;

    private Animator animator;
    private Rigidbody rigidBody;
    private const int LeftClick = 0;
    private bool isJump = false;
    private AudioSource audioSource;

    private void Awake()
    {
        Assert.IsNotNull(sfxJump, "Jump AudioClip is not initialized");
        Assert.IsNotNull(sfxDeath, "Death AudioClip is not initialized");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (!GameManager.instance.IsGameOver && GameManager.instance.IsGameStarted &&
            Input.GetMouseButtonDown(LeftClick))
        {
            GameManager.instance.PlayerStartedGame();

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") {
            rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            audioSource.PlayOneShot(sfxDeath);

            GameManager.instance.PlayerCollided();
        }
    }
}
