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
            settingsTabManager.PanelAnim(0);
        }

        Authentication.instance.OnLoggedIn += () =>
        {
            Debug.Log("Loggeddd");
            hasLoggedIn = true;
            settingsTabManager.PanelAnim(0);
        };
        Authentication.instance.OnSignedOut += () =>
        {
            Debug.Log("eeee");
            settingsTabManager.Panels[5].gameObject.SetActive(true);
            settingsTabManager.PanelAnim(5);
        };
    }
}
