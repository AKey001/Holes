using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using NativeAndroidElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    
    public void Open(GameObject gameObjectToOpen)
    {
        gameObjectToOpen.SetActive(true);
    }

    public void OpenWithGPGS(GameObject gameObjectToOpen)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            gameObjectToOpen.SetActive(true);    
        }
        else 
        {
            Toast.Show("Für diese Funktion sind Google Play Games Services notwendig.", Toast.LENGTH_SHORT);
        }
    }

    public void Close(GameObject gameObjectToClose)
    {
        gameObjectToClose.SetActive(false);
    }
}
