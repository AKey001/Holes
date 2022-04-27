using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    
    public void Open(GameObject gameObjectToOpen)
    {
        gameObjectToOpen.SetActive(true);
    }

    public void Close(GameObject gameObjectToClose)
    {
        gameObjectToClose.SetActive(false);
    }
}
