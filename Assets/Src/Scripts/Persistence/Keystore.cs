using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keystore
{

    public static string star1(int level)
    {
        return "star1_" + level;
    }
    
    public static string star2(int level)
    {
        return "star2_" + level;
    }
    
    public static string star3(int level)
    {
        return "star3_" + level;
    }
    
    public static string gyroEnabled()
    {
        return "gyroEnabled";
    }
    
    public static string musicVolume()
    {
        return "musicVolume";
    }
    
    public static string sfxVolume()
    {
        return "sfxVolume";
    }
    
    public static string quality()
    {
        return "quality";
    }
    
    public static string language()
    {
        return "language";
    }


    public static string time(int level)
    {
        return "bestTime_" + level;
    }
}
