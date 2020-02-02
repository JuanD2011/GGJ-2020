using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static async void GetLeaderboard()
    {
        List<Dictionary<string, string>> friendsDataList = await Database.DB.GetUserList(); 
        int friendsCount = friendsDataList.Count;

        for (int i = 0; i < friendsCount - 1; i++)
        {
            for (int j = 0; j < friendsCount - 1 - i; j++)
            {
                int actualFriendLevel = int.Parse(friendsDataList[j]["currentLevel"]);
                int nextFriendLevel = int.Parse(friendsDataList[j + 1]["currentLevel"]);
                if (actualFriendLevel < nextFriendLevel)
                {
                    friendsDataList[j]["currentLevel"] = nextFriendLevel.ToString();
                    friendsDataList[j + 1]["currentLevel"] = actualFriendLevel.ToString();
                }
            }
        }
        UILeaderboard.OnShowLeaderboard.Invoke(friendsDataList);
    }
}
