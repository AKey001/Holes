using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using NativeAndroidElements;
using TMPro;
using UnityEngine;

public class StatsLoader : MonoBehaviour
{
    public TextMeshProUGUI completedLevels;
    public TextMeshProUGUI falls;
    public TextMeshProUGUI attempts;
    public TextMeshProUGUI collectedStars;
    public TextMeshProUGUI playTimes;

    void Start()
    {
        LoadStats();    
    }

    public void LoadStats()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.FetchAllEvents(DataSource.ReadNetworkOnly, (status, list) =>
            {
                string Status = "Fetch All Status: " + status + "\n";
                print(Status);

                completedLevels.text = list[0].CurrentCount + "";
                falls.text = list[1].CurrentCount + "";
                attempts.text = list[2].CurrentCount + "";
                collectedStars.text = list[3].CurrentCount + "";
                playTimes.text = formatTime(list[4].CurrentCount);
                // playTimes.text = formatTime(9762938000); 112d 23h 55m 38s
                // time = 1122938;

            });
        }
        
        Toast.Show("Test", Toast.LENGTH_SHORT);
    }

    private string formatTime(float millis)
    {
        float time = millis / 1000f; // seconds
        float days = Mathf.FloorToInt(time / (60 * 60 * 24));
        time -= days * (60 * 60 * 24);
        float hours = Mathf.FloorToInt(time / (60 * 60));
        time -= hours * (60 * 60);
        float minutes = Mathf.FloorToInt(time / 60); 
        float seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:#0}d {1:#0}h {2:#0}m {3:#0}s", days, hours, minutes, seconds);
    }
}
