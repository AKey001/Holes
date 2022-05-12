using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class Star : MonoBehaviour
{
    public TimeManager timeManager;
    private void OnTriggerEnter(Collider other)
    {
        print("Star");
        timeManager.AddStar();
        enabled = false;
        
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_collected_stars, 1);
    }
}
