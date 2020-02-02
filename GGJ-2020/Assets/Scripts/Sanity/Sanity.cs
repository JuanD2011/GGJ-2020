using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private LeanTweenType easeType = LeanTweenType.linear;
    [SerializeField] private GameObject pavementsParent = null;
    [SerializeField] private CurrentLevelData levelData = null;

    [SerializeField] private OwnEventSystem.GameEvent OnGameOver = null;

    private Pavement[] pavements;

    private float insanityPercentage = 0f;

    private void Start()
    {
        pavements = pavementsParent.GetComponentsInChildren<Pavement>();
        foreach (Pavement pavement in pavements)
        {
            pavement.OnPavementDamaged += OnPavementDamaged; 
        }
    }

    private void OnDestroy()
    {
        foreach (Pavement pavement in pavements)
        {
            pavement.OnPavementDamaged -= OnPavementDamaged;
        }
    }

    private void OnPavementDamaged(float _damage)
    {
        insanityPercentage += (1f - levelData.pavementStatus) * _damage;
        LeanTween.value(fill.fillAmount, insanityPercentage, 1f).setEase(easeType).setOnUpdate((float _value) => UpdateFill(_value));
        
        if (insanityPercentage >= 1)
        {
            Debug.Log("GAME OVER");
            OnGameOver.Raise();
        }
    }

    private void UpdateFill(float _value)
    {
        fill.fillAmount = insanityPercentage;
    }
}
