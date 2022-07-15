
using UnityEngine;

public class SleepTimeout : MonoBehaviour
{
    public bool allowSleepTimeout;
    
    void Start()
    {
        if (allowSleepTimeout)
        {
            Screen.sleepTimeout = UnityEngine.SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = UnityEngine.SleepTimeout.NeverSleep;    
        }
        
    }

}
