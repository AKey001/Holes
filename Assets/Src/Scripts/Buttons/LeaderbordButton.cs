using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderbordButton : MonoBehaviour
{
    public void ShowLeaderbord(int id)
    {
        string GPGSid = "";
        switch (id)
        {
            case 1:
                GPGSid = GPGSIds.leaderboard_classic_wood;
                break;
            case 2:
                GPGSid = GPGSIds.leaderboard_the_one;
                break;
            case 3:
                GPGSid = GPGSIds.leaderboard_classic_wood;
                break;
            case 4:
                GPGSid = GPGSIds.leaderboard_classic_wood;
                break;
            case 5:
                GPGSid = GPGSIds.leaderboard_classic_wood;
                break;
        }
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSid);
    }
}
