
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] float timeSinceLastHitInSeconds = 2f;
    [SerializeField] Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;
    private ParticleSystem blood;

    void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
        blood = GetComponentInChildren<ParticleSystem>();
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
                blood.Play();
                timer = 0;
            }
        }
    }

    void TakeHit() {
        if (currentHealth > 0) {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
        }

        if (currentHealth <= 0) {
            KillPlayer();
        }
    }

    void KillPlayer() {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audio.PlayOneShot(audio.clip);
    }
}
