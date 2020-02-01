public class UIButtonHome : UIButtonBase
{
    public override void OnButtonClicked()
    {
        LevelManager.instance.LoadHome();
    }
}
