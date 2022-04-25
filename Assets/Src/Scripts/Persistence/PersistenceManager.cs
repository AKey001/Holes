using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PersistenceManager
{
    public static void SaveSettings(SettingState state)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.holes";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(fileStream, state);
        fileStream.Close();
    }

    public static SettingState LoadState()
    {
        string path = Application.persistentDataPath + "/settings.holes";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            SettingState state = formatter.Deserialize(fileStream) as SettingState;
            fileStream.Close();
            
            return state;
        }
        else
        {
            Debug.Log("File Not Found");
            return null;
        }
    }
    
}
