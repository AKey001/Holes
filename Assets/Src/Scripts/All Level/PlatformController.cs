using System;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Rigidbody platformRigidbody;

    public Joystick joystick;
    public JoystickHint hint;
    
    private void Awake()
    {
        platformRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        hint.Hide();
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

    void FixedUpdate()
    {
        if (gyroEnabled)
        {
            Vector3 eulerAngleVelocity = new Vector3(-gyro.rotationRate.x, 0, -gyro.rotationRate.y);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);    
            platformRigidbody.MoveRotation(platformRigidbody.rotation * deltaRotation);
        }
        else
        {
            Vector3 eulerAngleVelocity = new Vector3(joystick.Vertical, 0, -joystick.Horizontal);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
            platformRigidbody.MoveRotation(platformRigidbody.rotation * deltaRotation);
        }
    }
    
}