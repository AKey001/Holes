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
        float hundredths = (time % 1) * 100;
        if (hundredths < 101 && hundredths > 99)
        {
            hundredths = 0;
        }

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    public static float extractHundredths(float time)
    {
        float hundredths = (time % 1) * 100;
        if (hundredths < 101 && hundredths > 99)
        {
            hundredths = 0;
        }
        return hundredths;
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
