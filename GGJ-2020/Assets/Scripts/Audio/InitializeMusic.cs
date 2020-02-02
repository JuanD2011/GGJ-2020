using UnityEngine;

public class InitializeMusic : MonoBehaviour
{
    private static bool hasInitializeMusic = false;

    private void Start()
    {
        if (!hasInitializeMusic)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.audioClips.music, 1f, 1f, 2f); 
            hasInitializeMusic = true;
        }
    }
}
