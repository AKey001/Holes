using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickHint : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(!PersistenceManager.LoadSettings().gyroEnabled);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
        
}
