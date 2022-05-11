using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

public static class HighscoreLoader
{
    
    public static void LoadHighscore(int level, TextMeshProUGUI highscoreGUI)
    {
        string[] leaderboardIDs = {GPGSIds.leaderboard_classic_wood, GPGSIds.leaderboard_the_one};

        string highscore = "";
        
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(leaderboardIDs[level - 1], LeaderboardStart.TopScores,
                1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
                data =>
                {
                    if (data.Valid && data.PlayerScore != null)
                    {
                        highscoreGUI.text = TimeConverter.convertSeconds(data.PlayerScore.value / 1000f);    
                    }
                });
        }

    }

}
