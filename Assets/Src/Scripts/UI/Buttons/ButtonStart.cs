using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    public void ClaimAchievement()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_press_play_to_start, success =>
            {
                print("achievement unlocked: " + success);
            });
        }
    }
}
