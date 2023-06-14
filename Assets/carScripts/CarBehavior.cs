using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    public GameObject wheel_Left_F;
    public GameObject wheel_Right_F;
    public GameObject wheel_Real_Left_F;
    public GameObject wheel_Real_Right_F;
    public GameObject wheel_Real_Left_B;
    public GameObject wheel_Real_Right_B;
    private bool car_drive = true;
    private float front_wheel_torque = 2000f;
    private float back_wheel_torque = 2000f;
    private float front_wheel_force = 100f;
    private float back_wheel_force = 100f;
    public int Wheel_Turn_State = 0;
    private float turn_angle = 35f;
    private float speed_mod = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Car_Movement();
        Car_Reverse();
        Brakes_Control();
        Wheel_Turn();
        
    }
    private void Engine_Control (ref GameObject Wheel, float t, float f)
    {
        JointMotor motor = Wheel.GetComponent<HingeJoint>().motor;
        motor.targetVelocity = t;
        motor.force = f;
        Wheel.GetComponent<HingeJoint>().motor = motor;
    }
    private void Car_Movement()
    {
        if (Input.GetKey(KeyCode.I) & !Input.GetKey(KeyCode.K))
        {
            car_drive = true;
            if (Wheel_Turn_State == 0)
            {
                Engine_Control(ref wheel_Real_Left_F, front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Right_F, front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Left_B, back_wheel_torque, back_wheel_force);
                Engine_Control(ref wheel_Real_Right_B, back_wheel_torque, back_wheel_force);
            }
            else
            {
                Engine_Control(ref wheel_Real_Left_F, front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Right_F, front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Left_B, speed_mod*back_wheel_torque, speed_mod*back_wheel_force);
                Engine_Control(ref wheel_Real_Right_B, speed_mod*back_wheel_torque, speed_mod*back_wheel_force);
            }
        }
        else
        {
            car_drive = false;
            Engine_Control(ref wheel_Real_Left_F, 0, 0);
            Engine_Control(ref wheel_Real_Right_F, 0, 0);
            Engine_Control(ref wheel_Real_Left_B, 0, 0);
            Engine_Control(ref wheel_Real_Right_B, 0, 0);
        }
    }
    private void Brakes_On(ref GameObject Wheel)
    {
        Wheel.GetComponent<HingeJoint>().useMotor = false;
        Wheel.GetComponent<HingeJoint>().useSpring = true;
        JointSpring wheel_Spring = Wheel.GetComponent<HingeJoint>().spring;
        wheel_Spring.spring = 1000f;
        Wheel.GetComponent<HingeJoint>().spring = wheel_Spring;
    }
    private void Brakes_Off(ref GameObject Wheel)
    {
        Wheel.GetComponent<HingeJoint>().useSpring = false; 
        Wheel.GetComponent<HingeJoint>().useMotor = true;
        Engine_Control(ref Wheel, 0, 0);
    }
    private void Brakes_Control()
    {
        if (car_drive)
        {
            return;
        }
        if (Input.GetKey(KeyCode.B))
        {
            Brakes_On(ref wheel_Real_Left_F);
            Brakes_On(ref wheel_Real_Left_B);
            Brakes_On(ref wheel_Real_Right_F);
            Brakes_On(ref wheel_Real_Right_B);
        }
        else
        {
            Brakes_Off(ref wheel_Real_Left_F);
            Brakes_Off(ref wheel_Real_Left_B);
            Brakes_Off(ref wheel_Real_Right_F);
            Brakes_Off(ref wheel_Real_Right_B);
        }
    }
    private void Wheel_Turn()
    {
        if (Input.GetKey(KeyCode.J) & !Input.GetKey(KeyCode.L))
        {
            if (Wheel_Turn_State == 0)
            {
                wheel_Left_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler(turn_angle, 0f,  0f);
                wheel_Right_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler(turn_angle, 0f,  0f);
                Wheel_Turn_State = -1;
            }
        }
        if (!Input.GetKey(KeyCode.J) & Input.GetKey(KeyCode.L))
        {
            if (Wheel_Turn_State == 0)
            {
                wheel_Left_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( -turn_angle, 0f, 0f);
                wheel_Right_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( -turn_angle, 0f, 0f);
                Wheel_Turn_State = 1;
            }
        }
        if (!Input.GetKey(KeyCode.J) & !Input.GetKey(KeyCode.L))
        {
            if (Wheel_Turn_State == 1)
            {
                wheel_Left_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( turn_angle, 0f, 0f);
                wheel_Right_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( turn_angle, 0f, 0f);
                Wheel_Turn_State = 0;
            }
            if (Wheel_Turn_State == -1)
            {
                wheel_Left_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( -turn_angle, 0f, 0f);
                wheel_Right_F.GetComponent<ConfigurableJoint>().targetRotation *= Quaternion.Euler( -turn_angle, 0f, 0f);
                Wheel_Turn_State = 0;
            }
        }
    }
    private void Car_Reverse()
    {
        if (Input.GetKey(KeyCode.K) & !Input.GetKey(KeyCode.I))
        {
            car_drive = true;
            if  (Wheel_Turn_State == 0)
            {
                Engine_Control(ref wheel_Real_Left_F, -front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Right_F, -front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Left_B, -back_wheel_torque, back_wheel_force);
                Engine_Control(ref wheel_Real_Right_B, -back_wheel_torque, back_wheel_force);
            }
            else
            {
                Engine_Control(ref wheel_Real_Left_F, -front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Right_F, -front_wheel_torque, front_wheel_force);
                Engine_Control(ref wheel_Real_Left_B, -speed_mod * back_wheel_torque, speed_mod * back_wheel_force);
                Engine_Control(ref wheel_Real_Right_B, -speed_mod * back_wheel_torque, speed_mod * back_wheel_force);

            }
            
        }
        if ((!Input.GetKey(KeyCode.K) & !Input.GetKey(KeyCode.I)) || (!Input.GetKey(KeyCode.K) & !Input.GetKey(KeyCode.I)))
        {
            car_drive = false;
            Engine_Control(ref wheel_Real_Left_F, 0, 0);
            Engine_Control(ref wheel_Real_Right_F, 0, 0);
            Engine_Control(ref wheel_Real_Left_B, 0, 0);
            Engine_Control(ref wheel_Real_Right_B, 0, 0);
        }
    }
}
