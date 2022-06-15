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
    public Toggle[] toggles = new Toggle[3];
    public GyroButton gyroEnabled;

    void Awake()
    {
        Sync();
    }

    public void Sync()
    {
        language.SetValueWithoutNotify(PersistenceManager.LoadInt(Keystore.language(), 0));
        musicSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.musicVolume(), 1f));
        sfxSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.sfxVolume(), 1f));
        toggles[PersistenceManager.LoadInt(Keystore.quality(), 2)].SetIsOnWithoutNotify(true);
        gyroEnabled.SetValue(PersistenceManager.LoadBool(Keystore.gyroEnabled(), GyroscopeController.EnableGyroscope()));
    }
}
