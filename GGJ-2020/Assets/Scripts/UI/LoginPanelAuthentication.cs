using System.Collections;
using UnityEngine;

public class LoginPanelAuthentication : MonoBehaviour
{
    private SettingsTabManager settingsTabManager = null;

    private void Awake()
    {
        settingsTabManager = GetComponent<SettingsTabManager>();
    }

    private void Start()
    {
        Authentication.instance.OnLoggedIn += () => settingsTabManager.PanelAnim(0);
        Authentication.instance.OnSignedOut += () => settingsTabManager.PanelAnim(5);
    }
}
