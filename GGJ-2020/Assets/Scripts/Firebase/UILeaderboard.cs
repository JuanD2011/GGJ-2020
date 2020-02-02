using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILeaderboard : MonoBehaviour
{
    [SerializeField] Transform usersContainer = null;
    [SerializeField] GameObject leaderBoardHeader = null;

    public static System.Action<List<Dictionary<string, string>>> OnShowLeaderboard;

    private void OnEnable()
    {
        OnShowLeaderboard = ShowLeaderboard;
        Leaderboard.GetLeaderboard();
    }

    public void ShowLeaderboard(List<Dictionary<string, string>> friendsData)
    {
        usersContainer.gameObject.SetActive(true);
        int lenght = friendsData.Count > 10 ? 10 : friendsData.Count;
        
        for (int i = 0; i < lenght; i++)
        {
            //Instantiate(leaderBoardHeader, usersContainer);
            usersContainer.GetChild(i).gameObject.SetActive(true);
            usersContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{i + 1}.";
            usersContainer.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = friendsData[i]["name"];
            usersContainer.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = friendsData[i]["currentLevel"];
        }
    }
}
