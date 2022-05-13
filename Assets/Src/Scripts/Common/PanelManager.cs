using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using NativeAndroidElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    public GameObject parent;
    public CanvasGroup background;
    public GameObject popup;

    void Start()
    {
        background.alpha = 0;
        popup.transform.localScale = Vector3.zero;
    }

    public void Open()
    {
        parent.SetActive(true);
        background.LeanAlpha(1f, 0.05f).setEaseInSine();
        popup.LeanScale(Vector3.one, 0.25f).setEaseInQuart();
    }

    public void OpenWithGPGS()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Open();
        }
        else 
        {
            Toast.Show("Für diese Funktion sind Google Play Games Services notwendig.", Toast.LENGTH_SHORT);
        }
    }

    public void Close()
    {
        popup.LeanScale(Vector3.zero, 0.25f).setEaseInQuart().setOnComplete(o =>
        {
            parent.SetActive(false); 
        });
        background.LeanAlpha(0f, 0.05f).setEaseOutSine().setOnComplete(() =>
        {
            
        });
    }
}
