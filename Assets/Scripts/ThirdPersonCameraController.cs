using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothedSpeed = 0.125f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() {
        //Vector3 desiredPosition = target.position + offset;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed * Time.deltaTime);
    }
}