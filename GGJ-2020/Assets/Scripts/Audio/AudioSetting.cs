using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : UIButtonBase
{
    [SerializeField] private Settings settings = null;

    [SerializeField] private Image m_Image = null;
    [SerializeField] private Color enabledColor = new Color(0, 149, 135), disabledColor = Color.red;

    [SerializeField] private AudioType m_Type = AudioType.None;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Intialize UI
    /// </summary>
    public void OnEnable()
    {
        UpdateUI();
    }

    public override void OnButtonClicked()
    {
        AudioManager.Instance.MuteAudio(m_Type, UpdateUI);
    }

    private void UpdateUI()
    {
        switch (m_Type)
        {
            case AudioType.Music:
                if (settings.isMusicActive)
                {
                    m_Image.color = enabledColor;
                }
                else
                {
                    m_Image.color = disabledColor;
                }
                break;
            case AudioType.SFX:
                if (settings.isSFXActive)
                {
                    m_Image.color = enabledColor;
                }
                else
                {
                    m_Image.color = disabledColor;
                }
                break;
            default:
                break;
        }
    }
}