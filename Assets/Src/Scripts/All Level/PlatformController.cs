using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public Joystick joystick; 

    void Start()
    {
        gyroEnabled = EnableGyroscope();
        print("Gyro enabled=" + gyroEnabled);
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