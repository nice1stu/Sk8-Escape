using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping = 5f;
    public Vector3 offset;

    private Vector3 _velocity = Vector3.zero;
    
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        Vector3 desiredPosition = new Vector3(targetPosition.x + offset.x, transform.position.y, targetPosition.z + offset.z);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, damping);
    }
}