using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool paused = false;

    private void OnEnable()
    {
        paused = false;
    }

    public void SwitchPause(bool _value) => paused = _value;
}
