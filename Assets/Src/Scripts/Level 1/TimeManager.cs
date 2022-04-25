using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float time;
    private bool watchIsRunning;
    private bool timerIsRunning;
    private float timeRemaining = 4;
    private float overallMillis;
    private int starsCount;
    private int attemptsCount;
    private string watchTime;
    
    // Finish Panel
    public Text timeText;
    public Text bestTimeText;
    public Text attemptsText;

    // Telemetrics
    public TextMeshProUGUI watch;
    public TextMeshProUGUI countdown;
    public TextMeshProUGUI attempts;
    public TextMeshProUGUI stars;
    
    
    public GameObject ball;
    public GameObject platform;
    public GameObject countdownPanel;
    public GameObject FinishPanel;
    public GameObject PausePanel;
    
    public void Start()
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_play_time, (uint) overallMillis);
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_attempts, 1);
        
        time = 0;
        overallMillis = 0;
        timeRemaining = 4;
        attemptsCount = 0;
        timerIsRunning = true;
        watchIsRunning = false;
        watch.text = "00:00:00";
        attempts.text = "0";

        FinishPanel.SetActive(false);
        // ball.SetActive(false);
        platform.GetComponent<PlatformController>().enabled = false;
        countdownPanel.SetActive(true);
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update()
    {
        // timer
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            
            string remaining = string.Format("{0:0}", seconds);

            if (remaining == "0")
            {
                ball.SetActive(true);
                platform.GetComponent<PlatformController>().enabled = true;
                countdownPanel.SetActive(false);
                
                timeRemaining = 0;
                timerIsRunning = false;
                watchIsRunning = true;
            }
            else
            {
                countdown.text = remaining;
            }
        }
        
        // watch
        if (watchIsRunning)
        {
            time += Time.deltaTime;
            float minutes = Mathf.FloorToInt(time / 60); 
            float seconds = Mathf.FloorToInt(time % 60);
            float milliSeconds = (time % 1) * 100;
            if (milliSeconds < 101 && milliSeconds > 99)
            {
                milliSeconds = 0;
            }
            
            watchTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
            
            watch.text = watchTime;

            overallMillis = time * 1000;
            
            // print(string.Format("{0:000}", overallMillis ) + " | " + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds));
        }
    }

    public void AddAttempt()
    {
        attemptsCount++;
        attempts.text = attemptsCount.ToString();
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_attempts, 1);
    }
    
    public void AddStar()
    {
        starsCount++;
        stars.text = starsCount.ToString();
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_collected_stars, 1);
    }

    public void AddFall()
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_falls, 1);
    }
    
    public void Pause()
    {
        watchIsRunning = false;
        
        // TODO   ball
        // TODO   platform

        PausePanel.SetActive(true);
    }

    public void Finish()
    {
        watchIsRunning = false;
        
        timeText.text = watchTime;
        attemptsText.text = attemptsCount.ToString();
        
        
        
        FinishPanel.SetActive(true);
        
        
        ball.SetActive(false);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        platform.GetComponent<PlatformController>().enabled = false;
        
        // TODO reset platform to position not zero
        platform.transform.rotation = Quaternion.Euler(Vector3.zero);
        countdownPanel.SetActive(false);
        
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_completed_levels, 1);
        // save time to leaderboard
        PlayGamesPlatform.Instance.ReportScore((long) overallMillis, GPGSIds.leaderboard_classic_wood, b => {});
    }

    private void OnDestroy()
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_play_time, (uint) overallMillis);
    }
}
