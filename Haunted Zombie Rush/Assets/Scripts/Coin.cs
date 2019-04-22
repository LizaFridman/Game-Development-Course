using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Coin : Rock
{
    private MeshRenderer renderer;

    void Awake()
    {
        base.Awake();
        rotationSpeed = 0f;

        renderer = GetComponent<MeshRenderer>();
        Assert.IsNotNull(renderer);
    }
    void Start()
    {
        StartCoroutine(Move(bottomPosition));
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Reset()
    {
        base.Reset();
        //MaybeInvisible();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && renderer.enabled)
        {
            Vector3 newPosition = new Vector3(startPosition, transform.localPosition.y, transform.localPosition.z);
            transform.position = newPosition;
            //MaybeInvisible();
        }
    }

    public void MaybeInvisible() {
        if (Random.value > 0.5f)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
        }
    }
}