namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class PlayerStatusViewOverride
    {
        protected override void Patch()
        {
            //TODO：后面把Buff的视图优化一下

            //Tips
            var tips = UIStyle.GamePadTipsRectTransform(transform);
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.LeftAxisUpDownSlide,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.Exit,
                new[] { UIStyle.GamePadButton.Menu });
        }
    }
}
