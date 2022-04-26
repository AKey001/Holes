using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public Joystick joystick; 

    void Start()
    {
        joystick.enabled = false;
        gyroEnabled = EnableGyroscope();
        print("Gyro enabled=" + gyroEnabled);
        if (!gyroEnabled)
        {
            joystick.enabled = true;
        }
    }

    private bool EnableGyroscope()
    {
        if (!SystemInfo.supportsGyroscope) return false;
        gyro = Input.gyro;
        return gyro.enabled = true;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            transform.Rotate(gyro.rotationRate.x, 0, gyro.rotationRate.y);
        }
        else
        {
            transform.Rotate(-joystick.Vertical, 0, joystick.Horizontal);    
        }
        
    }
    
}