using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using NativeAndroidElements;
using UnityEngine;

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
            Toast.Show("Für diese Funktion sind Google Play Games Services notwendig.", Toast.LENGTH_SHORT);
        }

    }
    
}
