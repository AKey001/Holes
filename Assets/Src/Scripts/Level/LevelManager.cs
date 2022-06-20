
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
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

    public int level;
    
    // Finish Panel
    public Text timeText;
    public TextMeshProUGUI bestTimeText;
    public Text attemptsText;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    
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
        FindObjectOfType<AudioManager>().Stop("Theme");
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

                    FindObjectOfType<AudioManager>().Play("ActionTheme");
                    
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

                watch.text = watchTime = TimeConverter.convertSeconds(time);

                overallMillis = time * 1000;
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
            PlayGamesPlatform.Instance.Events.FetchEvent(DataSource.ReadCacheOrNetwork, GPGSIds.event_falls, (status, eventFalls) =>
            {
                Debug.Log(status);
                if (eventFalls.CurrentCount >= 1000 && eventFalls.CurrentCount < 1025)
                {
                    PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_you_dont_get_enough_arent_you);
                }
            });
        }
    }
    
    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        
        platform.GetComponent<PlatformController>().enabled = false;
        
        PausePanel.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        paused = false;
        
        platform.GetComponent<PlatformController>().enabled = true;

        PausePanel.SetActive(false);
    }
    
    public void LoadOtherScenePreparation()
    {
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Stop("Fall");
        FindObjectOfType<AudioManager>().Stop("ActionTheme");
        FindObjectOfType<AudioManager>().Play("Theme");
        
    }
    
    public void Finish()
    {
        watchIsRunning = false;

        // Audio
        FindObjectOfType<AudioManager>().Stop("ActionTheme");
        FindObjectOfType<AudioManager>().Play("Theme");
        
        // Finish Panel
        timeText.text = watchTime;
        attemptsText.text = attemptsCount.ToString();
        bestTimeText.text = "";  // Done hrough HighscoreLoader after reporting new score
        star1.SetActive(true);
        star2.SetActive(starsCount > 1);
        star3.SetActive(starsCount > 2);
        FinishPanel.SetActive(true);
        
        // ball
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.SetActive(false);
        
        // platform
        platform.GetComponent<PlatformController>().enabled = false;
        platform.transform.rotation = Quaternion.Euler(Vector3.zero);
        countdownPanel.SetActive(false);
        
        // leaderboard + events
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GlobalData.LevelCompletedAchievementID(level));
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_completed_levels, 1);
            PlayGamesPlatform.Instance.ReportScore((long) overallMillis, GlobalData.LeaderboardID(level), b =>
            {
                HighscoreLoader.LoadHighscore(level, bestTimeText);
            });

            if (attemptsCount < 2)
            {
                PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_first_try);
            }
        }
        
        // save Result 
        if (!PersistenceManager.LoadBool(Keystore.star1(level), false))
        {
            PersistenceManager.SaveBool(Keystore.star1(level), starsCount > 0);  
        }
        if (!PersistenceManager.LoadBool(Keystore.star2(level), false))
        {
            PersistenceManager.SaveBool(Keystore.star2(level), starsCount > 1);  
        }
        if (!PersistenceManager.LoadBool(Keystore.star3(level), false))
        {
            PersistenceManager.SaveBool(Keystore.star3(level), starsCount > 2);  
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
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_play_time, (uint) overallMillis);
            SavedGameManager.SaveGame();
        }

        Time.timeScale = 1;
    }
}
