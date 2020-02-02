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
        if (string.IsNullOrEmpty(tMPInputField.text)) return;

        Debug.Log($"{tMPInputField.text}");
        Authentication.instance.AnonymousAuth(tMPInputField.text);
        settingsTabManager.PanelAnim(0);
    }
}
