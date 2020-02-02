using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private int levelNumber = 0;

    private TextMeshProUGUI m_Text = null;
    private Button m_Button = null;

    private string id = "";

    /// <summary>
    /// Initialize level number, id, text and button
    /// </summary>
    /// <param name="_levelNumber"></param>
    public void Initialize(int _levelNumber)
    {
        m_Button = GetComponent<Button>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();

        id = $"Level{_levelNumber}";
        levelNumber = _levelNumber;
        m_Text.SetText($"Level {_levelNumber}");
        m_Button.onClick.AddListener(() =>
        {
            LevelManager.instance.LoadLevel(id);
            AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gunShot, 1f, false);
        });
    }
}
