using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject lockGameObject = null;

    private LevelsData levelsData = null;
    private CurrentLevelData currentLevelData = null;

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
        levelsData = Resources.Load<LevelsData>("ScriptableObjects/Levels/LevelsData");
        currentLevelData = Resources.Load<CurrentLevelData>("ScriptableObjects/Levels/CurrentLevelData");

        m_Button = GetComponent<Button>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();

        id = $"Level{_levelNumber}";
        levelNumber = _levelNumber;
        m_Text.SetText($"Level {_levelNumber}");

        lockGameObject.SetActive(false);
        m_Button.interactable = true;

        m_Button.onClick.AddListener(() =>
        {
            currentLevelData.levelData = levelsData.levelsData[levelNumber - 1];
            LevelManager.instance.LoadLevel("Level1");
            AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gunShot, 1f, false);
        });
    }
}
