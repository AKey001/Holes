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
        musicSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.musicVolume(), 1f));
        sfxSlider.SetValueWithoutNotify(PersistenceManager.LoadFloat(Keystore.sfxVolume(), 1f));
    }
}
