using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{

    public static int levelCount()
    {
        return 5;
    }

    public static string LeaderboardID(int level)
    {
        string[] ids = {
            GPGSIds.leaderboard_classic_wood, 
            GPGSIds.leaderboard_cardboard_tinkering, 
            GPGSIds.leaderboard_lost_in_the_middle,
            GPGSIds.leaderboard_remember_the_darkness};

        return ids[level];
    }

    public static string LevelCompletedAchievementID(int level)
    {
        string[] ids = {
            GPGSIds.achievement_that_was_easy, 
            GPGSIds.achievement_succesfully_tinkered, 
            GPGSIds.achievement_not_lost,
            GPGSIds.achievement_go_get_some_sun};
        
        return ids[level];
    }

}
