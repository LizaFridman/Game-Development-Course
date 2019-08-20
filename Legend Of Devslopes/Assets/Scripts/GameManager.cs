using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GameObject player;
    private bool isGameOver = false;

    public bool GameOver {
        get {
            return isGameOver;
        }
    }

    public GameObject Player {
        get {
            return player;
        }
    }

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit(int currentHp) {
        if(currentHp > 0)
        {
            isGameOver = false;
        }
        else {
            isGameOver = true;
        }
    }
}
