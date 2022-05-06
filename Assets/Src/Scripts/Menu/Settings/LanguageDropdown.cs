using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    public SettingsManager settingsManager;

    IEnumerator Start()
    {
        // Wait for the localization system to initialize, loading Locales, preloading etc.
        yield return LocalizationSettings.InitializationOperation;

        dropdown.onValueChanged.AddListener(i =>
        {
            settingsManager.ChangeLanguage(i);
        });
    }
    
}
