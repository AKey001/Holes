using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    public SettingsManager settingsManager;

    public void Start()
    {
        
        dropdown.onValueChanged.AddListener(index =>
        {
            if (index == 0)
            {
                settingsManager.ChangeGyro(true);    
            }
            else
            {
                settingsManager.ChangeGyro(false);
            }
            
        });
        
    }
}
