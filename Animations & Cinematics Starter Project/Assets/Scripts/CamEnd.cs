using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEnd : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        AnimationManager.instance.DestroyCamera(gameObject);
    }
}
