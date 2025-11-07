using Duckov.UI.Animations;
using DuckovController.Helper;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        [CanBeNull]
        private TMP_FontAsset _fontTemplate;

        public Canvas MenuCanvas { get; private set; }

        public RectTransform MenuButtonListLayout { get; private set; }

        public RectTransform MenuPadTipsLayout { get; private set; }

        public void Patch()
        {
            //获取参考
            if (!Utils.FindGameObject("Canvas", out Canvas canvas))
            {
                Debug.LogError("找不到Canvas");
                return;
            }
            MenuCanvas = canvas;
            var obj = canvas.transform.Find("MainMenuContainer/Menu/MainGroup/Layout");
            if (obj == null)
            {
                Debug.LogError("找不到主菜单的Layout");
                return;
            }
            MenuButtonListLayout = obj!.GetComponent<RectTransform>();
            if (MenuButtonListLayout == null)
            {
                Debug.LogError("找不到主菜单的Layout的RectTransform");
                return;
            }
            var tmp = MenuButtonListLayout.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null)
            {
                Debug.LogError("找不到模板字体");
                return;
            }
            _fontTemplate = tmp.font;

            MenuButtonListLayout.LogRectTransformInfo();
            UIStyle.currentFont = _fontTemplate;
            MenuPadTipsLayout = new GameObject("ControllerTips").AddComponent<RectTransform>();
            MenuPadTipsLayout.SetParent(MenuButtonListLayout.parent, false);
            MenuPadTipsLayout.anchorMin = new Vector2(1, 0);
            MenuPadTipsLayout.anchorMax = new Vector2(1, 0);
            MenuPadTipsLayout.pivot = new Vector2(1, 0);
            MenuPadTipsLayout.sizeDelta = new Vector2(500, 40);
            MenuPadTipsLayout.anchoredPosition = MenuButtonListLayout.anchoredPosition * new Vector2(-1, 1);
            var horGroup = MenuPadTipsLayout.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = 10;
            horGroup.childAlignment = TextAnchor.MiddleRight;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = false;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;
            horGroup.reverseArrangement = true;
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.A, "确认");
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.Up, "上");
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.Down, "下");

            //更改UI Hovering样式 改为描边嗷
            var btnAnims = MenuButtonListLayout.gameObject.GetComponentsInChildren<ButtonAnimation>();
            foreach (var buttonAnimation in btnAnims)
            {
                buttonAnimation.gameObject.AddComponent<MainMenuBtnStyleOverride>();
            }
        }
    }
}
