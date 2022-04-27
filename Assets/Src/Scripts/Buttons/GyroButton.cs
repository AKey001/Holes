using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GyroButton : MonoBehaviour
{
    public Button button;
    public RawImage buttonImage;
    public Texture2D gyro;
    public Texture2D joystick;
    public SettingsManager settingsManager;

    private bool disabled;
    private bool gyroEnabled;
    private Gyroscope gyroscope;
    
    private void Awake()
    {
        if (!EnableGyroscope())
        {
            disabled = true;
            button.interactable = false;
        }

        SetValue(gyroEnabled);
    }

    private bool EnableGyroscope()
    {
        if (!SystemInfo.supportsGyroscope) return false;
        gyroscope = Input.gyro;
        return gyroscope.enabled = true;
    }
    
    public void Click()
    {
        if (!disabled)
        {
            if (gyroEnabled)
            {
                buttonImage.texture = joystick;
                gyroEnabled = false;
                settingsManager.ChangeGyro(gyroEnabled);
            }
            else
            {
                buttonImage.texture = gyro;
                gyroEnabled = true;
                settingsManager.ChangeGyro(gyroEnabled);
            }
        }
    }

    public void SetValue(bool gyroEnabled)
    {
        this.gyroEnabled = gyroEnabled;
        if (disabled)
        {
            buttonImage.texture = joystick;
        }
        else
        {
            if (gyroEnabled)
            {
                buttonImage.texture = gyro;
            }
            else
            {
                buttonImage.texture = joystick;
            }   
        }
    }
}
