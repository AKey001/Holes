using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public TimeManager timeManager;
    
    public void ButtonRestart()
    {
        timeManager.Start();
    }
    
}
