using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeResultsUI : MonoBehaviour
{ 
    public List<UIResult> uiResults;
    

    private void Start()
    {
        List<ResultState> loadedResults = PersistenceManager.LoadResults();

        print(loadedResults.Count);
        
        for (int i = 0; i < loadedResults.Count; i++)
        {
            ResultState loadedResult = loadedResults[i];

            //UIResult uiResult = Array.Find(uiResults, uiResult => uiResult.level == loadedResult.level);
            
            foreach (var uiResult in uiResults)
            {
                if (uiResult.level == loadedResult.level)
                {
                    uiResult.star1.SetActive(loadedResult.star1);
                    uiResult.star2.SetActive(loadedResult.star2);
                    uiResult.star3.SetActive(loadedResult.star3);
                    uiResult.time.text = TimeConverter.convertSeconds(loadedResult.minutes * 60f + loadedResult.seconds + loadedResult.millis * 0.01f);
                    break;
                }
            }
            
        }
    }
}
