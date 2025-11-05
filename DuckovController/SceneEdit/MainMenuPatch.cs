using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit
{
    public class MainMenuPatch
    {
        public Canvas MenuCanvas { get; private set; } = null!;

        public RectTransform MenuButtonListLayout { get; private set; } = null!;

        public RectTransform MenuPadTipsLayout { get; private set; } = null!;

        public void Patch1()
        {
            //获取参考位置
            if (!Utils.Utils.FindGameObject("Canvas", out Canvas canvas))
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
            MenuPadTipsLayout.sizeDelta = new Vector2(200, 40);
            MenuPadTipsLayout.anchoredPosition = MenuButtonListLayout.anchoredPosition * new Vector2(-1, 1);
        }

        public void Patch2()
        {
            var horGroup = MenuPadTipsLayout.AddComponent<HorizontalLayoutGroup>();
            horGroup.childAlignment = TextAnchor.MiddleRight;
            UIStyle.DrawPadTips(MenuPadTipsLayout);
            UIStyle.DrawPadTips(MenuPadTipsLayout);
        }
    }
}
