using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    [SerializeField] int jumpHeight;

    private Rigidbody rigidBody;
    private Animator animator;

    //[SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(rigidBody);
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
    }

    // Update is called once per frame
    void Update()
    {
        var moveCharecter = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        transform.position += moveCharecter * Time.deltaTime * movementSpeed;

        if (rigidBody.velocity.magnitude == 0)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }

        if (GameManager.Instance.IsJumping)
        {
            animator.SetTrigger("Jump");
            rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            GameManager.Instance.IsJumping = false;
        }

        if (GameManager.Instance.IsPunching)
        {
            animator.SetTrigger("Punch");
            GameManager.Instance.IsPunching = false;
        }

        if (GameManager.Instance.IsBuilding)
        {
            animator.SetTrigger("Punch");
            GameManager.Instance.IsBuilding = false;
        }
    }
}
