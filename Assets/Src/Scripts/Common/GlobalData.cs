using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{

    public static int levelCount()
    {
        return 5;
    }

    public static string leaderboardID(int level)
    {
        string[] ids = {GPGSIds.leaderboard_classic_wood, GPGSIds.leaderboard_lost_in_the_middle};
        return ids[level];
    }

    }
