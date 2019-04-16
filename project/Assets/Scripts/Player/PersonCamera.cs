using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCamera : MonoBehaviour
{
    float pitch = 0.0f;
    float yaw = 0.0f;

    public float mouseSensitivity = 4.0f;
    public float distanceFromTarget = 2;
    public bool firstPerson = false;

    public Transform target;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -90, 90);

        Vector3 targetRotation = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = targetRotation;

        float direction = (firstPerson) ? 1 : -distanceFromTarget;
        transform.position = target.position + direction * transform.forward;
    }
}
