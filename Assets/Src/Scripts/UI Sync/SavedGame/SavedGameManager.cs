using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using NativeAndroidElements;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SavedGameManager : MonoBehaviour
{
    public List<UISavedGame> uiSavedGames;
    public GameObject loadingScreen;
    public GameObject noEntryHint;
    
    private List<ISavedGameMetadata> metadata;

    private static void OpenSave(Action<SavedGameRequestStatus, ISavedGameMetadata> action)
    {
        string filename = "Holes_" + SystemInfo.deviceUniqueIdentifier;
        OpenSaveWithFilname(filename, action);
    }
    
    private static void OpenSaveWithFilname(string filename, Action<SavedGameRequestStatus, ISavedGameMetadata> action)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
                    filename, 
                    DataSource.ReadNetworkOnly, 
                    ConflictResolutionStrategy.UseLongestPlaytime, 
                    action);
        }
    }
    
    public static void SaveGame()
    {
        OpenSave((status, metadata) =>
        {
            byte[] data = MakeData();

            SavedGameMetadataUpdate metadataUpdate = new SavedGameMetadataUpdate.Builder()
                .WithUpdatedDescription("" + DateTime.Now).Build();
                
            PlayGamesPlatform.Instance.SavedGame.CommitUpdate(metadata, metadataUpdate, data, SaveCallback);
        });
    }
    
    public void LoadSavedGame(int index)
    {
        OpenSaveWithFilname(metadata[index].Filename, (status, metadata) =>
        {
            PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(metadata, LoadCallback);
        });
    }

    public void DeleteGame(int index)
    {
        string message1 = "Den Spielstand von";
        string message2 = "wirklich unwiderruflich löschen?";
        string delete = "Löschen";
        string cancel = "Abbrechen";

        if (PersistenceManager.LoadInt(Keystore.language(), 0) == 1)
        {
            message1 = "Are you sure to delete the saved Game of";
            message2 = "?";
            delete = "Delete";
            cancel = "Cancel";
        }
        
        Alert alertDiaolog = new Alert();
        
        alertDiaolog.onSucessListener(() =>
        {
            loadingScreen.SetActive(true);
            Delete(metadata[index]);
            FetchSavedGames();
        });
        alertDiaolog.Show(delete, message1 + " " + metadata[index].Description + " " + message2, delete,cancel);
    }

    private void Delete(ISavedGameMetadata metadata)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.SavedGame.Delete(metadata);
        }
    }

    public void FetchSavedGames()
    {
        foreach (var uiSavedGame in uiSavedGames)
        {
            uiSavedGame.parent.SetActive(false);
        }
        noEntryHint.SetActive(false);
        
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.SavedGame.FetchAllSavedGames(DataSource.ReadNetworkOnly, (status, list) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    list.Sort((metadata1, metadata2) =>
                    {
                        return metadata2.LastModifiedTimestamp.CompareTo(metadata1.LastModifiedTimestamp);
                    });

                    for (int i = 5; i < list.Count; i++)
                    {
                        Delete(list[i]);
                    }

                    metadata = list;

                    for (int i = 0; i < list.Count; i++)
                    {
                        uiSavedGames[i].parent.SetActive(true);
                        uiSavedGames[i].description.text = list[i].Description;
                    }   
                }

                if (list.Count < 1)
                {
                    noEntryHint.SetActive(true);
                }
                loadingScreen.SetActive(false);
            });
        }
    }

    private void LoadCallback(SavedGameRequestStatus status, byte[] data)
    {
        loadingScreen.SetActive(false);
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Successfully loaded saved game");
            SyncData(data);
            Toast.Show("Success", Toast.LENGTH_SHORT);
        }
        else
        {
            Debug.Log("Failed to load saved game");
            Toast.Show("Failed", Toast.LENGTH_SHORT);
        }
    }

    private static void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved to cloud");
        }
        else
        {
            Debug.Log("Failed to save to cloud");
        }
    }
    
    private static byte[] MakeData()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();

        for (var i = 1; i < GlobalData.levelCount() + 1; i++)
        {
            data.Add(Keystore.star1(i), PersistenceManager.LoadBool(Keystore.star1(i), false));
            data.Add(Keystore.star2(i), PersistenceManager.LoadBool(Keystore.star2(i), false));
            data.Add(Keystore.star3(i), PersistenceManager.LoadBool(Keystore.star3(i), false));
            data.Add(Keystore.time(i), PersistenceManager.LoadString(Keystore.time(i), "not found"));
        }
            
        data.Add(Keystore.gyroEnabled(), PersistenceManager.LoadBool(Keystore.gyroEnabled(), GyroscopeController.EnableGyroscope()));
        data.Add(Keystore.musicVolume(), PersistenceManager.LoadFloat(Keystore.musicVolume(), 1f));
        data.Add(Keystore.sfxVolume(), PersistenceManager.LoadFloat(Keystore.sfxVolume(), 1f));
        data.Add(Keystore.quality(), PersistenceManager.LoadInt(Keystore.quality(), 2));
        data.Add(Keystore.language(), PersistenceManager.LoadInt(Keystore.language(), 0));
        
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, data);
        return stream.ToArray();
    }

    private void SyncData(byte[] data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        stream.Write(data, 0, data.Length);
        stream.Seek(0, SeekOrigin.Begin);
        Dictionary<string, object> loadedData = (Dictionary<string, object>) formatter.Deserialize(stream);

        for (var i = 1; i < GlobalData.levelCount() + 1; i++)
        {
            PersistenceManager.SaveBool(Keystore.star1(i), (bool) loadedData[Keystore.star1(i)]);
            PersistenceManager.SaveBool(Keystore.star2(i), (bool) loadedData[Keystore.star2(i)]);
            PersistenceManager.SaveBool(Keystore.star3(i), (bool) loadedData[Keystore.star3(i)]);
            PersistenceManager.SaveString(Keystore.time(i), (string) loadedData[Keystore.time(i)]);

        }
        
        PersistenceManager.SaveBool(Keystore.gyroEnabled(), (bool) loadedData[Keystore.gyroEnabled()]);
        PersistenceManager.SaveFloat(Keystore.musicVolume(), (float) loadedData[Keystore.musicVolume()]);
        PersistenceManager.SaveFloat(Keystore.sfxVolume(), (float) loadedData[Keystore.sfxVolume()]);
        PersistenceManager.SaveInt(Keystore.quality(), (int) loadedData[Keystore.quality()]);
        PersistenceManager.SaveInt(Keystore.language(), (int) loadedData[Keystore.language()]);

    }
}
