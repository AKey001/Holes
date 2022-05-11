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
            HighscoreLoader.LoadHighscore(uiResult.level, uiResult.time);
        }
        
        print("StartResultLoading");
        
        List<ResultState> loadedResults = PersistenceManager.LoadResults();

        if (loadedResults != null)
        {
            for (int i = 0; i < loadedResults.Count; i++)
            {
                ResultState loadedResult = loadedResults[i];
                
                foreach (var uiResult in uiResults)
                {
                    if (uiResult.level == loadedResult.level)
                    {
                        uiResult.star1.SetActive(loadedResult.star1);
                        uiResult.star2.SetActive(loadedResult.star2);
                        uiResult.star3.SetActive(loadedResult.star3);
                        break;
                    }
                }
            
            }   
        }
        
    }
    
}
