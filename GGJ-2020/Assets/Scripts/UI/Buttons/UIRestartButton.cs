public class UIRestartButton : UIButtonBase
{
    public override void OnButtonClicked()
    {
        LevelManager.instance.LoadSameLevel();
    }
}
