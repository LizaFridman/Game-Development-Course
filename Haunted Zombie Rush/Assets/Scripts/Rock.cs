using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Object
{
    [SerializeField] private Vector3 topPosition;
    [SerializeField] private Vector3 bottomPosition;

    private readonly float rockSpeed = 3f;
    private readonly float rotationSpeed = 90f;
    
    void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(bottomPosition));
    }

    protected override void Update()
    {
        if (GameManager.instance.IsPlayerActive)
        {
            // Movement from right to left
            base.Update();
        }
        transform.RotateAround(transform.localPosition, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Move the Rock Up-Down
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    IEnumerator Move(Vector3 targetPosition) {
        while (Mathf.Abs((targetPosition - transform.localPosition).y) > 0.2f) {
            Vector3 direction = (targetPosition.y == topPosition.y) ? Vector3.up : Vector3.down;
            transform.localPosition += direction * rockSpeed * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 newTarget = targetPosition.y == topPosition.y ? bottomPosition : topPosition;

        StartCoroutine(Move(newTarget));
    }
}
