using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Object
{
    [SerializeField] private Vector3 topPosition;
    [SerializeField] private Vector3 bottomPosition;
    private readonly float rockSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(bottomPosition));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Move(Vector3 targetPosition) {
        while (Mathf.Abs((targetPosition - transform.localPosition).y) > 0.2f) {
            Vector3 direction = (targetPosition.y == topPosition.y) ? Vector3.up : Vector3.down;
            transform.localPosition += direction * rockSpeed * Time.deltaTime;

            yield return null;
        }

        print("Reached the target");

        yield return new WaitForSeconds(0.5f);

        Vector3 newTarget = targetPosition.y == topPosition.y ? bottomPosition : topPosition;

        StartCoroutine(Move(newTarget));
    }
}
