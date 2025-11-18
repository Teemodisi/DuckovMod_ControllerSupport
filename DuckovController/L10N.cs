using DuckovController.Helper;

namespace DuckovController
{
    public class L10N : Singleton<L10N>
    {
        public string TitlePressAnyKey => "按[A]键继续";

        public string Confirm => "确认";

        public string Return => "返回";

        public string PickSelection => "选择";

        public string SlideLeft => "浏览左侧";

        public string SlideRight => "浏览右侧";

        public string Exit => "退出";

        public string MiniMapViewToolSwitchType => "切换样式";

        public string MiniMapViewZoom => "缩小放大";

        public string MiniMapViewMoveMap => "移动地图";

        public string MiniMapViewMoveCursor => "移动光标";

        public string MiniMapViewPinOrRemove => "标记/移除";

        private void Awake()
        {
            // LocalizationManager.SetOverrideText("Title", "");
        }
    }
}
