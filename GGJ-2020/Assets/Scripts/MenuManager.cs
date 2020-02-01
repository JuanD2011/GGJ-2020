using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Action<string> OnLoadLevel { get; internal set; }

    public void StartGame(string _level)
    {
        OnLoadLevel(_level);
    }
}
