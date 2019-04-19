using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private float objectSpeed = 1f;
     private float resetPosition = -24.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

        if (transform.localPosition.x <= resetPosition) {
            Vector3 newPosition = new Vector3(79.95f, transform.localPosition.y, transform.localPosition.z);
            transform.position = newPosition;
        }
    }
}
