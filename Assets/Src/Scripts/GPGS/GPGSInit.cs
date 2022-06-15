using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


public class GPGSInit : MonoBehaviour
{
    public static GPGSInit instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        } 
        DontDestroyOnLoad(gameObject); 
        instance = this;
        
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            print("authenticated: " + status);
            print(PlayGamesPlatform.Instance.GetUserId() + " " + PlayGamesPlatform.Instance.GetUserDisplayName());
        } else {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessManualAuthentication);
        }
    }
    
    internal void ProcessManualAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            print("authenticated: " + status);
            string text = PlayGamesPlatform.Instance.GetUserId() + " " + PlayGamesPlatform.Instance.GetUserDisplayName();
            print(text);
        }
    }
}
 