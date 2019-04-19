using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private float objectSpeed = 1f;
    [SerializeField] private float resetPosition = -24.8f;
    [SerializeField] private float startPosition = 79.95f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

        if (transform.localPosition.x <= resetPosition) {
            Vector3 newPosition = new Vector3(startPosition, transform.localPosition.y, transform.localPosition.z);
            transform.position = newPosition;
        }
    }
}
