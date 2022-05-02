using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AchievementsLoader : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public UIAchievement press_play;
    public UIAchievement first_try;
    public UIAchievement go_get_some_sun;
    
    
    void Start()
    {
        LoadAchievements();
    }

    public void LoadAchievements()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            int count = 0;
            Dictionary<string, bool> achievementsMapping = new Dictionary<string, bool>();
            PlayGamesPlatform.Instance.LoadAchievements(achievements =>
            {
                foreach (IAchievement achievement in achievements)
                {
                    achievementsMapping.Add(achievement.id, achievement.completed);
                    print(achievement.completed);
                    if (achievement.completed)
                    {
                        count++;
                    }
                }
                
                counter.text = count + " von " + achievementsMapping.Count;
                
                synchronizeUI(achievementsMapping);
            });
        }
    }

    private void synchronizeUI(Dictionary<string, bool> achievementsMapping)
    {
        if (achievementsMapping[GPGSIds.achievement_press_play_to_start])
        {
            press_play.name.color = Color.white;
            press_play.description.color = Color.white;
            press_play.locked.enabled = false;
            press_play.unlocked.enabled = true;
        }

        if (achievementsMapping[GPGSIds.achievement_first_try])
        {
            first_try.name.color = Color.white;
            first_try.description.color = Color.white;
            first_try.unlocked.enabled = true;
            first_try.locked.enabled = false;
        }

        if (achievementsMapping[GPGSIds.achievement_go_get_some_sun])
        {
            go_get_some_sun.name.color = Color.white;
            go_get_some_sun.description.color = Color.white;
            go_get_some_sun.locked.enabled = false;
            go_get_some_sun.unlocked.enabled = true;
        }
    }
}
