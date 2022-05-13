using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class QualityDropdown : Dropdown
{
    public void ChangeOptions()
    {
        List<string> list = new List<string> {"Gering", "Mittel", "Hoch"};
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            list = new List<string> {"Low", "Medium", "High"};
        }

        options.Clear();
        foreach (string option in list)
        {
            options.Add(new OptionData(option));
        }
        RefreshShownValue();
        
    }
}
