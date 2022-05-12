using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public static Init instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        } 
        DontDestroyOnLoad(gameObject); 
        instance = this;

        for (var i = 1; i < 6; i++)
        {
            HighscoreLoader.LoadHighscore(i);   
        }
    }
    
}
