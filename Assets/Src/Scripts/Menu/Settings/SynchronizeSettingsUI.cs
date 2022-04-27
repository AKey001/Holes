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
        SettingState state = PersistenceManager.LoadState();
        
        if (state == null) return;
        
        language.SetValueWithoutNotify(state.language);
        musicSlider.SetValueWithoutNotify(state.musicVolume);
        sfxSlider.SetValueWithoutNotify(state.sfxVolume);
        quality.SetValueWithoutNotify(state.quality);
        gyroEnabled.SetValue(state.gyroEnabled);
        
        
        print("sfx: " + state.sfxVolume + " music: " + state.musicVolume);
    }
}
