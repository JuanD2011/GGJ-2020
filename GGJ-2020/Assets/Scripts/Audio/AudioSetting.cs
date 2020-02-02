using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : UIButtonBase
{
    [SerializeField] private Settings settings = null;

    [SerializeField] private Image m_Image = null;
    [SerializeField] private Image muted = null;

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
                    muted.enabled = false;
                }
                else
                {
                    muted.enabled = true;
                }
                break;
            case AudioType.SFX:
                if (settings.isSFXActive)
                {
                    muted.enabled = false;
                }
                else
                {
                    muted.enabled = true;
                }
                break;
            default:
                break;
        }
    }
}