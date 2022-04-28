using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using waqashaxhmi.AndroidNativePlugin;

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
            AndroidNativePluginLibrary.Instance.ShowToast("Für diese Funktion sind Google Play Games Services notwendig.");
        }
    }

    public void Close(GameObject gameObjectToClose)
    {
        gameObjectToClose.SetActive(false);
    }
}
