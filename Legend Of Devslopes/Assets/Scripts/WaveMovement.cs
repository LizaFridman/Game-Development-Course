using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    [SerializeField] private float waveHeight;
    [SerializeField] private float tideSpeed;

    private Vector3 orgPosition;
    private Vector3 minPosition;
    private Vector3 maxPosition;

    private bool isMovingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        orgPosition = transform.position;
        minPosition = orgPosition + (Vector3.down * waveHeight);
        maxPosition = orgPosition + (Vector3.up * waveHeight);

        StartCoroutine(MoveUp());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator MoveUp()
    {
        Debug.Log("Moving Up");

        while (IsFirstAboveSecond(maxPosition, transform.position, Vector3.up))
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPosition, Time.deltaTime * tideSpeed);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Finished Moving Up");
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        Debug.Log("Moving Down");

        while (IsFirstAboveSecond(transform.position, minPosition, Vector3.up))
        {
            transform.position = Vector3.MoveTowards(transform.position, minPosition, Time.deltaTime * tideSpeed);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Finished Moving Down");
        StartCoroutine(MoveUp());
    }

    public static bool IsFirstAboveSecond(Vector3 a, Vector3 b, Vector3 up)
    {
        //return Vector3.Dot(b - a, up) < 0;
        return (up.y > 0) ? (a.y > b.y) : (a.y < b.y);
    }
}
