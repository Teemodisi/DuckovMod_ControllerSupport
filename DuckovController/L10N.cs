using DuckovController.Helper;

namespace DuckovController
{
    public class L10N : Singleton<L10N>
    {
        private void Awake()
        {
            
            // LocalizationManager.SetOverrideText("Title", "");
        }

        public string TitlePressAnyKey => "按[A]键继续";

        public string NavigateUp => "上一个";

        public string NavigateDown => "下一个";

        public string Confirm => "确认";

        public string Return => "返回";
    }
}
