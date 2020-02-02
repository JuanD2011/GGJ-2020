using UnityEngine;

public class MenuGameManager : MonoBehaviour
{
    public void OnVictory()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.winner, 1f, false);
    }

    public void OnDefeat()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gameOver, 1f, false);
    }
}
