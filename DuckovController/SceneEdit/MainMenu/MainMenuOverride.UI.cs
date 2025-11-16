using Duckov.UI.Animations;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        [CanBeNull]
        private TMP_FontAsset _fontTemplate;

        public RectTransform MenuButtonListLayout { get; private set; }

        public RectTransform MenuPadTipsLayout { get; private set; }

        public void Patch()
        {
            //目前位置 Canvas/MainMenuContainer/Menu/MainGroup
            var obj = transform.Find("Layout");
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
            UIStyle.currentFont = _fontTemplate;

            MenuPadTipsLayout = new GameObject("ControllerTips").AddComponent<RectTransform>();
            var canvasGroup = MenuPadTipsLayout.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            MenuPadTipsLayout.SetParent(MenuButtonListLayout.parent, false);
            MenuPadTipsLayout.pivot = new Vector2(1, 0);
            MenuPadTipsLayout.anchorMin = new Vector2(0, 0);
            MenuPadTipsLayout.anchorMax = new Vector2(1, 0);
            MenuPadTipsLayout.anchoredPosition = new Vector2(-100, 50);
            MenuPadTipsLayout.sizeDelta = new Vector2(0, UIStyle.tips_rect_height);
            var horGroup = MenuPadTipsLayout.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = 10;
            horGroup.childAlignment = TextAnchor.MiddleRight;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = true;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, L10N.Instance.MenuMainNavigate,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, L10N.Instance.Confirm,
                new[] { UIStyle.GamePadButton.A });

            //更改UI Hovering样式 改为描边嗷
            var btnAnims = MenuButtonListLayout.gameObject.GetComponentsInChildren<ButtonAnimation>();
            foreach (var buttonAnimation in btnAnims)
            {
                buttonAnimation.gameObject.AddComponent<MainMenuBtnStyleOverride>();
            }
        }

        private void OnFadeGroupCompleted(FadeGroup fadeGroup)
        {
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
        }
    }
}
