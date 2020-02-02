using TMPro;
using UnityEngine;

public class UIButtonLogIn : UIButtonBase
{
    [SerializeField] private SettingsTabManager settingsTabManager = null;
    [SerializeField] private TMP_InputField tMPInputField = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnButtonClicked()
    {
        if (string.IsNullOrEmpty(tMPInputField.text))
        {
            Authentication.instance.AnonymousAuth("Guest");
        }
        else 
        {
            Authentication.instance.AnonymousAuth(tMPInputField.text);
        }

        Debug.Log($"{tMPInputField.text}");
        settingsTabManager.PanelAnim(0);
    }
}
