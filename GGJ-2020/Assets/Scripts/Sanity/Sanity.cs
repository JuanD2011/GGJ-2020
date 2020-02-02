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

        float random = Random.Range(0f, 1f);

        if (random > 0.5f)
        {
            int clip = Random.Range(0, AudioManager.Instance.audioClips.angryCharacter.Length);
            AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.angryCharacter[clip], 1f, false);
        }
        
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
