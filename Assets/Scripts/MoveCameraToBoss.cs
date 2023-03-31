using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToBoss : MonoBehaviour
{
    public float x, y, z;
    public Transform target;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(x, y, z));
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}
