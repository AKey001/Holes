using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PersistenceManager
{
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    
    public static void SaveBool(string key, bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);    
        }
    }

    public static void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    
    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    
    
    
    public static int LoadInt(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }
    
    public static bool LoadBool(string key, bool defaultValue)
    {
        if (defaultValue)
        {
            return PlayerPrefs.GetInt(key, 1) == 1;
        }
        return PlayerPrefs.GetInt(key, 0) == 1;    
        
    }

    public static float LoadFloat(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
    
    public static string LoadString(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }
    
}
