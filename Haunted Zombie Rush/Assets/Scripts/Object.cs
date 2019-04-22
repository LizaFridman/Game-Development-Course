using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float resetPosition = -24.8f;
    [SerializeField] protected float startPosition = 79.95f;

    protected Vector3 originalPosition;

    protected void Awake()
    {
        originalPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.instance.IsGameOver)
        {
            transform.Translate(Vector3.left * (horizontalSpeed * Time.deltaTime), Space.World);

            if (transform.localPosition.x <= resetPosition)
            {
                Vector3 newPosition = new Vector3(startPosition, transform.localPosition.y, transform.localPosition.z);
                transform.position = newPosition;
            }
        }
    }

    public virtual void Reset()
    {
        transform.position = originalPosition;
        gameObject.SetActive(true);
    }
}
