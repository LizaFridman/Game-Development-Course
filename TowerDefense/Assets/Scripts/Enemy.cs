using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exitPoint;
    public Transform[] waypoints;
    public float navigationUpdate;

    private Transform enemyLocation;
    private float navigationTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyLocation = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null) {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate) {
                if (target < waypoints.Length)
                {
                    enemyLocation.position = Vector2.MoveTowards(enemyLocation.position, waypoints[target].position, navigationTime);
                }
                else {
                    enemyLocation.position = Vector2.MoveTowards(enemyLocation.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            target++;
        }
        else if(other.tag == "Finish"){
            GameManager.Instance.RemoveEnemyFromScreen();
            Destroy(gameObject);
        }
    }
}