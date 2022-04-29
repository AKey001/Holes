using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mixer;
    private SettingState state;

    IEnumerator Start()
    {
        state = PersistenceManager.LoadSettings();
        if (state == null)
        {
            state = new SettingState
            {
                language = 0,
                quality = 2,
                gyroEnabled = true,
                sfxVolume = 1f,
                musicVolume = 1f
            };
        }
        ChangeMusicVolume(state.musicVolume);
        ChangeSFXVolume(state.sfxVolume);
        ChangeQuality(state.quality);
        ChangeGyro(state.gyroEnabled);
        
        yield return LocalizationSettings.InitializationOperation;
        ChangeLanguage(state.language);
    }

    private void UpdateSavedState()
    {
        PersistenceManager.SaveSettings(state);
    }

    public void ChangeMusicVolume(float volume)
    {
        print(volume);
        state.musicVolume = volume;
        volume = Mathf.Log10(volume) * 20;
        mixer.SetFloat("music", volume);
        UpdateSavedState();
    }
    
    public void ChangeSFXVolume(float volume)
    {
        print(volume);
        state.sfxVolume = volume;
        volume = Mathf.Log10(volume) * 20;
        mixer.SetFloat("sfx", volume);
        UpdateSavedState();
    }

    public void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        state.language = index;
        UpdateSavedState();
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        state.quality = index;
        UpdateSavedState();
    }

    public void ChangeGyro(bool gyroEnabled)
    {
        state.gyroEnabled = gyroEnabled;
        UpdateSavedState();
    }

}
