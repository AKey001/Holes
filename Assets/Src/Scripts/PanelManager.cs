using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    
    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
