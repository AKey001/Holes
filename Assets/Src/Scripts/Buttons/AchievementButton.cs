using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using waqashaxhmi.AndroidNativePlugin;

public class AchievementButton : MonoBehaviour
{

    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();   
        }
        else
        {
            AndroidNativePluginLibrary.Instance.ShowToast("Für diese Funktion sind Google Play Games Services notwendig.");
        }

    }
    
}
