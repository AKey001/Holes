using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mixer;

    IEnumerator Start()
    {
        ChangeMusicVolume(PersistenceManager.LoadFloat(Keystore.musicVolume(), 1f));
        ChangeSFXVolume(PersistenceManager.LoadFloat(Keystore.sfxVolume(), 1f));
        ChangeQuality(PersistenceManager.LoadInt(Keystore.quality(), 2));
        ChangeGyro(PersistenceManager.LoadBool(Keystore.gyroEnabled(), GyroscopeController.EnableGyroscope()));
        
        yield return LocalizationSettings.InitializationOperation;
        ChangeLanguage(PersistenceManager.LoadInt(Keystore.language(), 0));
    }
    

    public void ChangeMusicVolume(float volume)
    {
        PersistenceManager.SaveFloat(Keystore.musicVolume(), volume);
        
        volume = Mathf.Log10(volume) * 20;
        mixer.SetFloat("music", volume);
    }
    
    public void ChangeSFXVolume(float volume)
    {
        PersistenceManager.SaveFloat(Keystore.sfxVolume(), volume);
        
        volume = Mathf.Log10(volume) * 20;
        mixer.SetFloat("sfx", volume);
    }

    public void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        PersistenceManager.SaveInt(Keystore.language(), index);

    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        
        PersistenceManager.SaveInt(Keystore.quality(), index);

    }

    public void ChangeGyro(bool gyroEnabled)
    {
        PersistenceManager.SaveBool(Keystore.gyroEnabled(), gyroEnabled);

    }

}
