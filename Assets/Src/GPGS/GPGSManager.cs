using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


public class GPGSManager : MonoBehaviour
{
    void Awake()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);

        // if (PlayGamesPlatform.Instance.IsAuthenticated())
        // {
        //     string text = PlayGamesPlatform.Instance.GetUserId() + " " + PlayGamesPlatform.Instance.GetUserDisplayName();
        //     print(text);
        // }
        //PlayGamesPlatform.Instance.ManuallyAuthenticate(null);
    }

    void Start()
    {
        // if (PlayGamesPlatform.Instance.IsAuthenticated())
        // {
        //     PlayGamesPlatform.Instance.LoadScores(
        //         GPGSIds.leaderboard_classic_wood,
        //         LeaderboardStart.PlayerCentered,
        //         1,
        //         LeaderboardCollection.Public,
        //         LeaderboardTimeSpan.AllTime,
        //         (data) =>
        //         {
        //             print(data);
        //             // mStatus = "Leaderboard data valid: " + data.Valid;
        //             // mStatus += "\n approx:" +data.ApproximateCount + " have " + data.Scores.Length;
        //         }); 
        // }
    }


    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            // Continue with Play Games Services
            print("authenticated: " + status);
            string text = PlayGamesPlatform.Instance.GetUserId() + " " + PlayGamesPlatform.Instance.GetUserDisplayName();
            print(text);
        } else {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ManualAuthentication);
        }
    }
    
    internal void ManualAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            // Continue with Play Games Services
            print("authenticated: " + status);
            string text = PlayGamesPlatform.Instance.GetUserId() + " " + PlayGamesPlatform.Instance.GetUserDisplayName();
            print(text);
        } else {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
        }
    }
}
 