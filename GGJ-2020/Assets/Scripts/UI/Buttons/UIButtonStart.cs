public class UIButtonStart : UIButtonBase
{
    public override void OnButtonClicked()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gunReload, 1f, false);
    }
}
