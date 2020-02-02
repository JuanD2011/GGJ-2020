public class UIButtonStart : UIButtonBase
{
    public override void OnButtonClicked()
    {
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.cat, 1f, false);
    }
}
