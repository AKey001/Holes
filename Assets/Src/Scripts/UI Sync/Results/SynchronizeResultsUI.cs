using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

public class SynchronizeResultsUI : MonoBehaviour
{
    public List<UIResult> uiResults;

    private void Start()
    {
        foreach (var uiResult in uiResults)
        {
            string time = PersistenceManager.LoadString(Keystore.time(uiResult.level), "not found");
            if (time == "not found")
            {
                HighscoreLoader.LoadHighscore(uiResult.level, uiResult.time);   
            }
            else
            {
                uiResult.time.text = time;
            }

            uiResult.star1.SetActive(PersistenceManager.LoadBool(Keystore.star1(uiResult.level), false));
            uiResult.star2.SetActive(PersistenceManager.LoadBool(Keystore.star2(uiResult.level), false));
            uiResult.star3.SetActive(PersistenceManager.LoadBool(Keystore.star3(uiResult.level), false));
        }
    }
    
}
