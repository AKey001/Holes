using System;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Rigidbody rigidbody;

    public Joystick joystick;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        joystick.enabled = false;
        gyroEnabled = PersistenceManager.LoadSettings().gyroEnabled;
        print("Gyro enabled=" + gyroEnabled);
        if (!gyroEnabled)
        {
            joystick.enabled = true;
        }
        else
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }

    void Update()
    {
        // transform.Rotate(0.001f, 0.001f, 0.001f);
        // transform.Rotate(-0.001f, -0.001f, -0.001f);
        
        if (gyroEnabled)
        {
            Vector3 eulerAngleVelocity = new Vector3(gyro.rotationRate.x, 0, gyro.rotationRate.y);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);    
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
            // transform.Rotate(gyro.rotationRate.x, 0, gyro.rotationRate.y);
        }
        else
        {
            Vector3 eulerAngleVelocity = new Vector3(-joystick.Vertical, 0, joystick.Horizontal);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
            // transform.Rotate(-joystick.Vertical, 0, joystick.Horizontal);    
        }
        
    }
    
}