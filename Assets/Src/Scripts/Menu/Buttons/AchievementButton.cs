using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class AchievementButton : MonoBehaviour
{

    public void ShowAchievements()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    
}
