using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderboardsLoader : MonoBehaviour
{
    public List<UIPlayer> players;
    public UIPlayer selfPlayer;
    public GameObject loadingScreen;
    public GameObject noEntryHint;

    public void LoadLeaderboards(int level)
    {
        noEntryHint.SetActive(false);
        
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.LoadScores(GlobalData.LeaderboardID(level), LeaderboardStart.TopScores,
                10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
                data =>
                {
                    if (data.Valid)
                    {
                        int count = 0;
                        List<string> userIDs = new List<string>(); 
                        for (int i = 0; i < 10; i++)
                        {
                            if (data.Scores.Length > i)
                            {
                                players[i].parent.SetActive(true);
                                userIDs.Add(data.Scores[i].userID); 
                                players[i].time.text = TimeConverter.convertSeconds(data.Scores[i].value / 1000f);

                                players[i].parent.GetComponent<Image>().color = new Color32(34, 39, 47, 255);
                                
                                count++;
                            }
                            else
                            {
                                players[i].parent.SetActive(false);
                            }
                        }

                        if (count < 1)
                        {
                            loadingScreen.SetActive(false);
                            noEntryHint.SetActive(true);
                        }
                        else
                        {
                            noEntryHint.SetActive(false);
                        }

                        PlayGamesPlatform.Instance.LoadUsers(userIDs.ToArray(), users =>
                        {
                            for (int i = 0; i < users.Length; i++)
                            {
                                players[i].name.text = users[i].userName;
                            }
                            loadingScreen.SetActive(false);
                        });
                        
                        

                        if (data.PlayerScore != null)
                        {
                            if (data.PlayerScore.rank <= 10)
                            {
                                selfPlayer.parent.SetActive(false);
                                players[data.PlayerScore.rank - 1].parent.GetComponent<Image>().color = new Color32(68, 77, 91, 100);
                            }
                            else
                            {
                                selfPlayer.parent.SetActive(true);
                                selfPlayer.rank.text = data.PlayerScore.rank.ToString();
                                selfPlayer.time.text = TimeConverter.convertSeconds(data.PlayerScore.value / 1000f);
                                selfPlayer.name.text = PlayGamesPlatform.Instance.GetUserDisplayName();
                            }
                        }

                    }
                });
        }
    }
}
