using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    public float mouseSensitivity = 100f;
    public float topClamp = -90f;
    public float bottomClamp = 90f;

    float xRotation = 0f;
    float yRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0, yRotation, 0f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0f);
    }
}
