using DuckovController.Helper;

namespace DuckovController
{
    public class L10N : Singleton<L10N>
    {
        public string TitlePressAnyKey => "按[A]键继续";

        public string MenuMainNavigate => "上/下导航";

        public string Confirm => "确认";

        public string Return => "返回";

        public string MiniMapViewToolSwitchType => "切换样式";

        public string MiniMapViewToolSelect => "左/右选择";

        public string MiniMapViewZoom => "放大缩小";

        public string MiniMapViewMoveMap => "移动地图";

        public string MiniMapViewMoveCursor => "移动光标";

        public string MiniMapViewPinOrRemove => "标记/移除";

        private void Awake()
        {
            // LocalizationManager.SetOverrideText("Title", "");
        }
    }
}
