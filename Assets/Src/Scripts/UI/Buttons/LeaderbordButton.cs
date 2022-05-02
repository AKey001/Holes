using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
        
        // PlayGamesPlatform.Instance.LoadScores(
        //     GPGSIds.leaderboard_classic_wood,
        //     LeaderboardStart.PlayerCentered,
        //     100,
        //     LeaderboardCollection.Public,
        //     LeaderboardTimeSpan.AllTime,
        //     (data) =>
        //     {
        //         print("Leaderboard data valid: " + data.Valid);
        //         print("\n approx:" +data.ApproximateCount + " have " + data.Scores.Length);
        //         print("Score " + data.PlayerScore);
        //     });
        //
        
       
    }
    
}
