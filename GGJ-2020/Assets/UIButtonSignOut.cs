public class UIButtonSignOut : UIButtonBase
{
    public override void OnButtonClicked()
    {
        Authentication.instance.SignOut();
    }
}
