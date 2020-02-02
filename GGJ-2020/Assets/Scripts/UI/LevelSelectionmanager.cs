using UnityEngine;

public class LevelSelectionmanager : MonoBehaviour
{
    [SerializeField] private Transform levelContent = null;

    private LevelSelection[] levelSelections = null;

    private async void Awake()
    {
        levelSelections = levelContent.GetComponentsInChildren<LevelSelection>();

        int currentLevel = await Database.DB.GetCurrentLevel();

        levelSelections[0].Initialize(1);

        for (int i = 1; i < currentLevel; i++)
        {
            levelSelections[i].Initialize(i + 1);
        }
    }
}
