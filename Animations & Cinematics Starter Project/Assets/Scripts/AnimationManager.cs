using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance = null;

    [SerializeField] private List<GameObject> cameras = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    void StartNextCamera() {
            cameras[0].SetActive(true);
    }

    public void DestroyCamera(GameObject camera) {
        if (cameras.Count > 1)
        {
            cameras.RemoveAt(0);
            Destroy(camera);
            StartNextCamera();
        }
    }
}
