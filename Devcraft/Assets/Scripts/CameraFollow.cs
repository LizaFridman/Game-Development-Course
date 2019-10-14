using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distanceFromTarget;
    [SerializeField] float targetHeight;

    private float x = 0;
    private float y = 0;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) {
            return;
        }

        y = target.eulerAngles.y;
        var rotation = Quaternion.Euler(x,y,0);
        transform.rotation = rotation;

        var position = target.position - (rotation * Vector3.forward * distanceFromTarget + new Vector3(0, -targetHeight, 0));
        transform.position = position;

    }
}
