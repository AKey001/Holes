using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynchronizePauseSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
        SettingState state = PersistenceManager.LoadState();
        
        if (state == null) return;
        
        musicSlider.SetValueWithoutNotify(state.musicVolume);
        sfxSlider.SetValueWithoutNotify(state.sfxVolume);
    }
}
