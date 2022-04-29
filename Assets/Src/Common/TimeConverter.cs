using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class TimeConverter
{
    public static string convertSeconds(float time)
    {
        // time = 1033586956838.24f;
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliSeconds = (time % 1) * 100;
        if (milliSeconds < 101 && milliSeconds > 99)
        {
            milliSeconds = 0;
        }
        
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
    }

    public static float extractMillis(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliSeconds = (time % 1) * 100;
        if (milliSeconds < 101 && milliSeconds > 99)
        {
            milliSeconds = 0;
        }
        return milliSeconds;
    }
    
    public static float extractSeconds(float time)
    {
        return Mathf.FloorToInt(time % 60);
    }
    
    public static float extractMinutes(float time)
    {
        return Mathf.FloorToInt(time / 60f);
    }
}
