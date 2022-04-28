using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeResultsUI : MonoBehaviour
{ 
    public List<UIResult> uiResults;


    private void Awake()
    {
        int starsCount = 1;
        int time = 43643654;
        
        
        ResultState currentResult = new ResultState();
        currentResult.star1 = false;
        currentResult.star2 = false;
        currentResult.star3 = false;
        if (starsCount > 0)
        {
            currentResult.star1 = true;    
        }
        if (starsCount > 1)
        {
            currentResult.star2 = true;    
        }
        if (starsCount > 2)
        {
            currentResult.star3 = true;    
        }
        currentResult.time = time;
        currentResult.level = 1;
        
        
        List<ResultState> resultStates = PersistenceManager.LoadResults();
        bool exists = false;
        foreach (var loadedResult in resultStates)
        {
            if (loadedResult.level == 1)
            {
                if (loadedResult.star1 == true)
                {
                    currentResult.star1 = false;
                }
                if (loadedResult.star2 == true)
                {
                    currentResult.star2 = false;
                }
                if (loadedResult.star3 == true)
                {
                    currentResult.star3 = false;
                }
                if (loadedResult.time < currentResult.time)
                {
                    currentResult.time = loadedResult.time;
                }
                resultStates.Remove(loadedResult);
                break;
            }
        }

        currentResult.time = 1033586956838.24d;
        resultStates.Add(currentResult);   
        PersistenceManager.SaveResults(resultStates);
    }

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
                    uiResult.time.text = TimeConverter.convertSeconds((float) loadedResult.time);
                    break;
                }
            }
            
        }
    }
}
