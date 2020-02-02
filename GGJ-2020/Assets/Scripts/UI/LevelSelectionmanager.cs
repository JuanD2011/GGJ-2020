using UnityEngine;

public class LevelSelectionmanager : MonoBehaviour
{
    [SerializeField] private Transform levelContent = null;

    private LevelSelection[] levelSelections = null;

    private void Awake()
    {
        levelSelections = levelContent.GetComponentsInChildren<LevelSelection>();

        for (int i = 0; i < levelSelections.Length; i++)
        {
            levelSelections[i].Initialize(i + 1);
        }
    }
}
