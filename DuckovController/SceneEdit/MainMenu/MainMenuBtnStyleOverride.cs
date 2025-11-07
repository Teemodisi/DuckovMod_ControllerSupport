using Duckov.UI.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.MainMenu
{
    public class MainMenuBtnStyleOverride : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private ButtonAnimation _btnAnimation;

        private void Awake()
        {
            Patch();
            _btnAnimation = gameObject.GetComponent<ButtonAnimation>();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _btnAnimation.OnPointerExit(null);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _btnAnimation.OnPointerEnter(null);
        }

        private void Patch()
        {
            var hoveringObj = gameObject.transform.Find("Hovering");
            if (hoveringObj == null)
            {
                //为什么退出游戏按钮是不一样的样式 Tell me why！
                hoveringObj = gameObject.transform.Find("Image/Hovering");
            }
            if (hoveringObj != null)
            {
                var image = hoveringObj.GetComponent<Image>();
                image.color = Color.white;
                var rt = hoveringObj.GetComponent<RectTransform>();
                rt.offsetMin += new Vector2(-5, -5);
                rt.offsetMax += new Vector2(5, 5);
                var pi = hoveringObj.GetComponent<ProceduralImage>();
                pi.BorderWidth = 5;
                var um = hoveringObj.GetComponent<UniformModifier>();
                um.Radius += 5;
            }
        }
    }
}
