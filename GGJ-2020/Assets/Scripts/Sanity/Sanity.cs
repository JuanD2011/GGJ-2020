using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private LeanTweenType easeType = LeanTweenType.linear;
    [SerializeField] private Pavement pavement = null;
    [SerializeField] private CurrentLevelData levelData = null;

    [SerializeField] private OwnEventSystem.GameEvent OnGameOver = null;

    private float insanityPercentage = 0f;

    private void OnEnable()
    {
        pavement.OnPavementDamaged += OnPavementDamaged;
    }

    private void OnDisable()
    {
        pavement.OnPavementDamaged += OnPavementDamaged;
    }

    private void OnPavementDamaged(float _damage)
    {
        insanityPercentage += (1f - levelData.pavementStatus) * _damage;
        StartCoroutine(UpdateFill());
        
        if (insanityPercentage >= 1)
        {
            Debug.Log("GAME OVER");
            OnGameOver.Raise();
        }
    }

    private IEnumerator UpdateFill()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            fill.fillAmount = Mathf.Lerp(fill.fillAmount, insanityPercentage, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
