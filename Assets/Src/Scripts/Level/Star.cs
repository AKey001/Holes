using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class Star : MonoBehaviour
{
    public LevelManager levelManager;
    private void OnTriggerEnter(Collider other)
    {
        print("Star");
        levelManager.AddStar();
        enabled = false;
        
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_collected_stars, 1);
    }
}
