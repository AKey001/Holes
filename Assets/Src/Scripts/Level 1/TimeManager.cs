using System;
using System.Collections.Generic;
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
    private float timeRemaining;
    private float overallMillis;
    private int starsCount;
    private int attemptsCount;
    private string watchTime;
    private bool paused;
    
    // Finish Panel
    public Text timeText;
    public Text bestTimeText;
    public Text attemptsText;

    // Telemetrics
    public TextMeshProUGUI watch;
    public TextMeshProUGUI countdown;
    public TextMeshProUGUI attempts;
    public TextMeshProUGUI stars;
    
    // Gameobjects
    public GameObject ball;
    public GameObject platform;
    public GameObject countdownPanel;
    public GameObject FinishPanel;
    public GameObject PausePanel;
    
    public void Start()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_attempts, 1);   
        }

        time = 0;
        overallMillis = 0;
        timeRemaining = 4;
        attemptsCount = 0;
        timerIsRunning = true;
        watchIsRunning = false;
        watch.text = "00:00:00";
        attempts.text = "0";
        paused = false;

        // ball.GetComponent<Rigidbody>().useGravity = false;
        // FinishPanel.SetActive(false);
        // ball.SetActive(false);
        // platform.GetComponent<PlatformController>().enabled = false;
        // countdownPanel.SetActive(true);
        // ball.SetActive(true);
        // ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update()
    {
        if (!paused)
        {

            // timer
            if (timerIsRunning)
            {
                float secondsBefore = Mathf.FloorToInt(timeRemaining % 60);
                string remainingBefore = string.Format("{0:0}", secondsBefore);
                
                timeRemaining -= Time.deltaTime;
                
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                string remainingNew = string.Format("{0:0}", seconds);

                
                if (!remainingNew.Equals(remainingBefore))
                {
                    if (remainingNew == "0")
                    {
                        FindObjectOfType<AudioManager>().Play("Countdown Start");  
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Countdown");
                    }
                }
                
                if (remainingNew == "0")
                {
                    platform.GetComponent<PlatformController>().enabled = true;
                    countdownPanel.SetActive(false);
                    // ball.GetComponent<Rigidbody>().useGravity = true;

                    timeRemaining = 0;
                    timerIsRunning = false;
                    watchIsRunning = true;
                }
                else
                {
                    countdown.text = remainingNew;
                }
            }

            // watch
            if (watchIsRunning)
            {
                time += Time.deltaTime;
                // float minutes = Mathf.FloorToInt(time / 60);
                // float seconds = Mathf.FloorToInt(time % 60);
                // float milliSeconds = (time % 1) * 100;
                // if (milliSeconds < 101 && milliSeconds > 99)
                // {
                //     milliSeconds = 0;
                // }
                //
                // watchTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
                
                // watch.text = watchTime;

                watch.text = TimeConverter.convertSeconds(time);

                overallMillis = time * 1000;

                // print(string.Format("{0:000}", overallMillis ) + " | " + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds));
            }
        }
    }

    public void AddAttempt()
    {
        attemptsCount++;
        attempts.text = attemptsCount.ToString();

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_attempts, 1);   
        }
    }
    
    public void AddStar()
    {
        starsCount++;
        stars.text = starsCount.ToString();
        
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_collected_stars, 1);   
        }
    }

    public void AddFall()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_falls, 1);
        }
    }
    
    public void Pause()
    {
        print(Time.timeScale);
        Time.timeScale = 0;
        paused = true;
        
        // ball.GetComponent<Rigidbody>().useGravity = false;
        platform.GetComponent<PlatformController>().enabled = false;

        PausePanel.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        paused = false;
        
        // ball.GetComponent<Rigidbody>().useGravity = true;
        platform.GetComponent<PlatformController>().enabled = true;

        PausePanel.SetActive(false);
    }
    
    public void Finish()
    {
        watchIsRunning = false;
        
        // Finish Panel
        timeText.text = watchTime;
        attemptsText.text = attemptsCount.ToString();
        bestTimeText.text = "00:00:00";  // TODO best level time
        FinishPanel.SetActive(true);
        
        
        // ball
        ball.SetActive(false);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        // platform
        platform.GetComponent<PlatformController>().enabled = false;
        platform.transform.rotation = Quaternion.Euler(Vector3.zero); // TODO reset platform to position not zero
        countdownPanel.SetActive(false);
        
        // save Result
        ResultState currentResult = new ResultState();
        currentResult.star1 = false;
        currentResult.star2 = false;
        currentResult.star3 = false;
        if (starsCount > 0)
        {
            currentResult.star1 = true;    
        }
        if (starsCount > 1)
        {
            currentResult.star2 = true;    
        }
        if (starsCount > 2)
        {
            currentResult.star3 = true;    
        }
        currentResult.time = time;
        currentResult.level = 1;
        
        
        List<ResultState> resultStates = PersistenceManager.LoadResults();
        bool exists = false;
        foreach (var loadedResult in resultStates)
        {
            if (loadedResult.level == 1)
            {
                if (loadedResult.star1 == true)
                {
                    currentResult.star1 = true;
                }
                if (loadedResult.star2 == true)
                {
                    currentResult.star2 = true;
                }
                if (loadedResult.star3 == true)
                {
                    currentResult.star3 = true;
                }
                if (loadedResult.time < currentResult.time)
                {
                    currentResult.time = loadedResult.time;
                }
                resultStates.Remove(loadedResult);
                break;
            }
        }

        resultStates.Add(currentResult);   
        PersistenceManager.SaveResults(resultStates);
        
        // leaderbord + events
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_completed_levels, 1);
            PlayGamesPlatform.Instance.ReportScore((long) overallMillis, GPGSIds.leaderboard_classic_wood, b => { });
        }
    }

    public void Calibrate()
    {
        platform.transform.rotation = Quaternion.Euler(Vector3.zero);
        Vector3 position = ball.transform.position;
        position.y = 0.03f;
        ball.transform.position = position;
    }

    private void OnDestroy()
    {
        print("time counted, time manager destroyed");
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_play_time, (uint) overallMillis);
        }

        Time.timeScale = 1;
    }
}
