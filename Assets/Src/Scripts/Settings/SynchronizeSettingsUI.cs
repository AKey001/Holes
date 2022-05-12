using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynchronizeSettingsUI : MonoBehaviour
{
    public Dropdown language;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Dropdown quality;
    public GyroButton gyroEnabled;

    void Awake()
    {
        language.SetValueWithoutNotify(PersistenceManager.LoadInt(Keystore.language(), 0));
        musicSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.musicVolume(), 1f));
        sfxSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.sfxVolume(), 1f));
        quality.SetValueWithoutNotify(PersistenceManager.LoadInt(Keystore.quality(), 2));
        gyroEnabled.SetValue(PersistenceManager.LoadBool(Keystore.gyroEnabled(), GyroscopeController.EnableGyroscope()));
    }
}
