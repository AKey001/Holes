using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    // public AudioMixer musicMixer;
    // public AudioMixer sfxMixer;
    private SettingState state;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        state = PersistenceManager.LoadState();
        if (state == null)
        {
            state = new SettingState
            {
                language = 0,
                quality = 2,
                gyroEnabled = true,
                sfxVolume = 0f,
                musicVolume = 0f
            };
        }
        //ChangeMusicVolume(state.musicVolume);
        //ChangeSFXVolume(state.sfxVolume);
        ChangeLanguage(state.language);
        ChangeQuality(state.quality);
        ChangeGyro(state.gyroEnabled);
    }
    
    private void UpdateSavedState()
    {
        PersistenceManager.SaveSettings(state);
    }

    // public void ChangeMusicVolume(float volume)
    // {
    //     musicMixer.SetFloat("music", volume);
    //     state.musicVolume = volume;
    //     UpdateSavedState();
    // }
    //
    // public void ChangeSFXVolume(float volume)
    // {
    //     sfxMixer.SetFloat("sfx", volume);
    //     state.sfxVolume = volume;
    //     UpdateSavedState();
    // }

    public void ChangeLanguage(int index)
    {
        print(index);
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
