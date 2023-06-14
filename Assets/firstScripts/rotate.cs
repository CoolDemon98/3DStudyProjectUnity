using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float sensivity = 600f;

    public Transform Body;
    float xRotation =0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -94f, 84f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Body.Rotate(mouseX * Vector3.up);


        // yRotation -= mouseX;



    }
}
