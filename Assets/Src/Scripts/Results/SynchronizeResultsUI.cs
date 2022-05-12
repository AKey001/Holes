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
        
        
        
        // print("StartResultLoading");
        //
        // List<ResultState> loadedResults = PersistenceManager.LoadResults();
        //
        // if (loadedResults != null)
        // {
        //     for (int i = 0; i < loadedResults.Count; i++)
        //     {
        //         ResultState loadedResult = loadedResults[i];
        //         
        //         foreach (var uiResult in uiResults)
        //         {
        //             if (uiResult.level == loadedResult.level)
        //             {
        //                 uiResult.star1.SetActive(loadedResult.star1);
        //                 uiResult.star2.SetActive(loadedResult.star2);
        //                 uiResult.star3.SetActive(loadedResult.star3);
        //                 break;
        //             }
        //         }
        //     
        //     }   
        // }
        
    }
    
}
