#nullable disable
using System;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit
{
    [Obsolete]
    public class MainMenuPatch
    {
        public Canvas MenuCanvas { get; private set; }

        public RectTransform MenuButtonListLayout { get; private set; }

        public RectTransform MenuPadTipsLayout { get; private set; }

        public void Patch1()
        {
            //获取参考位置
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
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout);
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout);
        }
    }
}
