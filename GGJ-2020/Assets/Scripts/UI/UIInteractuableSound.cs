using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractuableSound : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }

    private void PlaySound()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.defaultButton, 1f, false);
    }
}
