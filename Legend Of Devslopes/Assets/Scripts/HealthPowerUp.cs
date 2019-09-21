using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    private GameObject _player;
    private PlayerHealth _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance.Player;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player) {
            _playerHealth.PowerUpHealth();
            Destroy(gameObject);
        }
    }
}
