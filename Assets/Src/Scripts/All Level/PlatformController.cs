using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public Joystick joystick; 

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