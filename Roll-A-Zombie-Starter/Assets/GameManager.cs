using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int selectedZombiePosition = 0;
    public GameObject selectedZombie;
    public List<GameObject> zombies;
    public Vector3 selectedSize;
    public Vector3 defaultSize;

    // Start is called before the first frame update
    void Start()
    {
        SelectZombie(selectedZombie);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left")) {
            GetZombieLeft();
        }

        if (Input.GetKeyDown("right"))
        {
            GetZombieRight();
        }

        if (Input.GetKeyDown("up"))
        {
            PushUp();
        }
    }

    void GetZombieLeft()
    {
        if (selectedZombiePosition == 0)
        {
            selectedZombiePosition = zombies.Count - 1;
            //SelectZombie(zombies[selectedZombiePosition]);
        }
        else {
            selectedZombiePosition = selectedZombiePosition - 1;
            //var newZombie = zombies[selectedZombiePosition];
            //SelectZombie(newZombie);
        }

        SelectZombie(zombies[selectedZombiePosition]);
    }

    void GetZombieRight()
    {
        if (selectedZombiePosition == zombies.Count - 1)
        {
            selectedZombiePosition = 0;
            
        }
        else
        {
            selectedZombiePosition = selectedZombiePosition + 1;
            //var newZombie = zombies[selectedZombiePosition];
            //SelectZombie(newZombie);
        }
        SelectZombie(zombies[selectedZombiePosition]);
    }

    void PushUp()
    {
        var rigidBody = selectedZombie.GetComponent<Rigidbody>();
        rigidBody.AddForce(0,0,10, ForceMode.Impulse);
    }

    void SelectZombie(GameObject newZombie) {
        selectedZombie.transform.localScale = defaultSize;
        selectedZombie = newZombie;
        newZombie.transform.localScale = selectedSize;
    }
}
