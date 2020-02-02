using System.Collections;
using UnityEngine;

public class LoginPanelAuthentication : MonoBehaviour
{
    private SettingsTabManager settingsTabManager = null;

    private static bool hasLoggedIn = false;

    private void Awake()
    {
        settingsTabManager = GetComponent<SettingsTabManager>();
    }

    private void Start()
    {
        if (hasLoggedIn)
        {
            settingsTabManager.Panels[5].gameObject.SetActive(false);
            settingsTabManager.PanelAnim(0);
        }

        Authentication.instance.OnLoggedIn += () =>
        {
            hasLoggedIn = true;
            settingsTabManager.Panels[5].gameObject.SetActive(false);
            settingsTabManager.PanelAnim(0);
        };
        Authentication.instance.OnSignedOut += () =>
        {
            settingsTabManager.Panels[5].gameObject.SetActive(true);
            settingsTabManager.PanelAnim(5);
        };
    }
}
