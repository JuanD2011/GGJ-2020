using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool paused = false;

    public void SwitchPause()
    {
        if (paused) paused = false;
        else paused = true;
    }
}
