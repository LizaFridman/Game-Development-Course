using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    [SerializeField] int jumpHeight;

    private Rigidbody rigidBody;
    private Animator animator;
    private AudioSource audioSource;

    //[SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(rigidBody);
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        var moveCharecter = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        if (moveCharecter != Vector3.zero)
        {
            animator.SetBool("IsWalking", true);
            var targetRotation = Quaternion.LookRotation(moveCharecter, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        transform.position += moveCharecter * Time.deltaTime * movementSpeed;

        if (GameManager.Instance.IsJumping)
        {
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(AudioManager.Instance.Jump);
            rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            GameManager.Instance.IsJumping = false;
        }

        if (GameManager.Instance.IsPunching)
        {
            animator.SetTrigger("Punch");
            audioSource.PlayOneShot(AudioManager.Instance.Hit);
            ModifyTerrain.Instance.DestroyBlock(10f, (byte)TextureType.Air.GetHashCode());
            GameManager.Instance.IsPunching = false;
        }

        if (GameManager.Instance.IsBuilding)
        {
            animator.SetTrigger("Punch");
            audioSource.PlayOneShot(AudioManager.Instance.Build);
            ModifyTerrain.Instance.AddBlock(10f, (byte)TextureType.Rock.GetHashCode());// can change the texture depending on the input
            GameManager.Instance.IsBuilding = false;
        }
    }
}

