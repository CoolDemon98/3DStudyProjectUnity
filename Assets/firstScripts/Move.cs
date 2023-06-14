using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float currentSpeed = 12f;
    public float sprintSpeed  = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;


    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGround;

     void Update()
    {
                isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGround && velocity.y < 0){
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift)){
        speed = sprintSpeed;
        }
            else{
                speed = currentSpeed;
            }


        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * - 2f * gravity);
        }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime); 
    }
}
