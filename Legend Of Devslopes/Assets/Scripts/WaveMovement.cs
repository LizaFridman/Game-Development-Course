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
        while (IsFirstAboveSecond(maxPosition, transform.position, Vector3.up))
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPosition, Time.deltaTime * tideSpeed);
            yield return new WaitForEndOfFrame();
        }

        yield return StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (IsFirstAboveSecond(transform.position, minPosition, Vector3.up))
        {
            transform.position = Vector3.MoveTowards(transform.position, minPosition, Time.deltaTime * tideSpeed);
            yield return new WaitForEndOfFrame();
        }

        yield return StartCoroutine(MoveUp());
    }

    public static bool IsFirstAboveSecond(Vector3 first, Vector3 second, Vector3 upDirecction)
    {
        return (upDirecction.y > 0) ? (first.y > second.y) : (first.y < second.y);
    }


}
