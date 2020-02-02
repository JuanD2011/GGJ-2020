using UnityEngine;

public class MenuGameManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.city, 0.3f, true);
    }

    public void OnVictory()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.winner, 1f, false);
    }

    public void OnDefeat()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gameOver, 1f, false);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.StopByClip(AudioManager.Instance.audioClips.city);
    }
}
