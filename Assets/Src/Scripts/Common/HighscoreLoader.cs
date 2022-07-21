using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class HighscoreLoader
{
    
    public static void LoadHighscore(int level, TextMeshProUGUI highscoreGUI)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(GlobalData.LeaderboardID(level), LeaderboardStart.TopScores,
                1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
                data =>
                {
                    if (data.Valid && data.PlayerScore != null)
                    {
                        string time = TimeConverter.convertSeconds(data.PlayerScore.value / 1000f);
                        if (!highscoreGUI.IsUnityNull()) { highscoreGUI.text = time; }
                        PersistenceManager.SaveString(Keystore.time(level), time);
                    }
                });
        }
    }

    public static void LoadHighscore(int level)
    {
        LoadHighscore(level, null);
    }


}
