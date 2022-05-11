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
        float deltax = 0;
        float deltaz = 0;
        if (gyroEnabled)
        {
            deltax = -gyro.rotationRate.x;
            deltaz = -gyro.rotationRate.y;
        }
        else
        {
            deltax = joystick.Vertical;
            deltaz = -joystick.Horizontal;
        }
        
        float x = platformRigidbody.rotation.eulerAngles.x;
        float z = platformRigidbody.rotation.eulerAngles.z;
        
        if (z < 180 && z > 40) // zu weit nach rechts
        {
            if (deltaz > 0)
            {
                deltaz = 0;
            }
        }
        if (z > 180 && z < 320) // zu weit nach links
        {
            if (deltaz < 0)
            {
                deltaz = 0;
            }
        }
        if (x < 180 && x > 40) // zu weit nach vorn
        {
            if (deltax > 0)
            {
                deltax = 0;
            }
        }
        if (x > 180 && x < 320) // zu weit nach hinten
        {
            if (deltax < 0)
            {
                deltax = 0;
            }
        }
        
        Vector3 eulerAngleVelocity = new Vector3(deltax, 0, deltaz);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
            
        print((platformRigidbody.rotation * deltaRotation).eulerAngles);

        platformRigidbody.MoveRotation(platformRigidbody.rotation * deltaRotation);
        
        
        // if (gyroEnabled)
        // {
        //     //if (platformRigidbody.rotation.eulerAngles.z )
        //     Vector3 eulerAngleVelocity = new Vector3(-gyro.rotationRate.x, 0, -gyro.rotationRate.y);
        //     Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
        //     
        //     print((platformRigidbody.rotation * deltaRotation).eulerAngles);
        //
        //     platformRigidbody.MoveRotation(platformRigidbody.rotation * deltaRotation);
        // }
        // else
        // {
        //     Vector3 eulerAngleVelocity = new Vector3(joystick.Vertical, 0, -joystick.Horizontal);
        //     Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
        //     
        //     print((platformRigidbody.rotation * deltaRotation).eulerAngles);
        //     
        //     platformRigidbody.MoveRotation(platformRigidbody.rotation * deltaRotation);
        // }
    }
    
}