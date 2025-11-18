namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class NoteIndexViewOverride
    {
        protected override void Patch()
        {
            var tips = UIStyle.GamePadTipsRectTransform(transform);
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.PickSelection,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.SlideLeft,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.SlideRight,
                new[] { UIStyle.GamePadButton.RightAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.Exit,
                new[] { UIStyle.GamePadButton.Menu });
        }
    }
}
