using Duckov.UI.Animations;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        [CanBeNull]
        private TMP_FontAsset _fontTemplate;

        private RectTransform _menuButtonListLayout;

        private RectTransform _menuPadTipsLayout;

        protected override void Patch()
        {
            //目前位置 Canvas/MainMenuContainer/Menu/MainGroup
            var obj = transform.Find("Layout");
            if (obj == null)
            {
                Debug.LogError("找不到主菜单的Layout");
                return;
            }
            _menuButtonListLayout = obj!.GetComponent<RectTransform>();
            if (_menuButtonListLayout == null)
            {
                Debug.LogError("找不到主菜单的Layout的RectTransform");
                return;
            }
            var tmp = _menuButtonListLayout.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null)
            {
                Debug.LogError("找不到模板字体");
                return;
            }
            _fontTemplate = tmp.font;
            UIStyle.currentFont = _fontTemplate;

            _menuPadTipsLayout = UIStyle.GamePadTipsRectTransform(_menuButtonListLayout.parent);
            UIStyle.DrawPadButtonTips(_menuPadTipsLayout, L10N.Instance.DpadUpDownSelect,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(_menuPadTipsLayout, L10N.Instance.Confirm,
                new[] { UIStyle.GamePadButton.A });

            //更改UI Hovering样式 改为描边嗷
            var btnAnims = _menuButtonListLayout.gameObject.GetComponentsInChildren<ButtonAnimation>();
            foreach (var buttonAnimation in btnAnims)
            {
                buttonAnimation.gameObject.AddComponent<MainMenuBtnStyleOverride>();
            }
        }
    }
}
