using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _exitPoint;
    [SerializeField]
    private Transform[] _waypoints;
    [SerializeField]
    private float _navigationUpdate;
    [SerializeField]
    private int _healthPoints;
    [SerializeField]
    private int _rewardAmount;

    private int _target = 0;
    private Transform _enemyLocation;
    private Collider2D _enemyCollider;
    private Animator _animator;

    private float _navigationTime = 0;
    private bool _isDead = false;

    public bool IsDead
    {
        get
        {
            return _isDead;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyLocation = GetComponent<Transform>();
        _enemyCollider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        GameManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_waypoints != null && !_isDead) {
            _navigationTime += Time.deltaTime;
            if (_navigationTime > _navigationUpdate) {
                if (_target < _waypoints.Length)
                {
                    _enemyLocation.position = Vector2.MoveTowards(_enemyLocation.position, _waypoints[_target].position, _navigationTime);
                }
                else {
                    _enemyLocation.position = Vector2.MoveTowards(_enemyLocation.position, _exitPoint.position, _navigationTime);
                }
                _navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            _target++;
        }
        else if (other.tag == "Finish") {
            GameManager.Instance.RoundEscaped++;
            GameManager.Instance.TotalEscaped++;

            GameManager.Instance.Unregister(this);
            GameManager.Instance.UpdateGameState();
        } else if (other.tag == "Projectile") {
            var projectile = other.gameObject.GetComponent<Projectile>();
            Destroy(other.gameObject);

            if (projectile != null) {
                Hit(projectile.AttackStrength);
            }
        }
    }

    public void Hit(int hitPoints) {
        if (_healthPoints - hitPoints > 0)
        {
            _healthPoints -= hitPoints;
            _animator.Play("Hurt");
        }
        else {
            _animator.SetTrigger("DidDie");
            Die();
        }
    }

    public void Die() {
        _isDead = true;
        _enemyCollider.enabled = false;
        GameManager.Instance.TotalKilled++;
        GameManager.Instance.AddMoney(_rewardAmount);
        GameManager.Instance.UpdateGameState();
    }
}