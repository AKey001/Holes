using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public void ChangeMusicVolume(float volume)
    {
        musicMixer.SetFloat("music", volume);
    }
    
    public void ChangeSFXVolume(float volume)
    {
        sfxMixer.SetFloat("sfx", volume);
    }

    public void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
    
}
