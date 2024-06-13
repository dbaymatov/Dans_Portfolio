using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    [Range(0, 10)]
    public float smoothFactor;


    void FixedUpdate()
    {
        Follow();
    }


    void Follow()
    {

        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothedPosition;

    }

}
