using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit
{
    public static class UIStyle
    {
        public static RectTransform DrawPadTips(RectTransform parent)
        {
            var rectTransform = new GameObject("GamePadTips").AddComponent<RectTransform>();
            rectTransform.SetParent(parent);
            rectTransform.sizeDelta = new Vector2(120, 40);

            DrawButtonCircle(rectTransform);

            var label = NewLabel("Label",rectTransform);
            label.rectTransform.pivot = new Vector2(1, 0.5f);
            label.rectTransform.anchorMin = new Vector2(1, 0);
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.sizeDelta = new Vector2(80, 40);

            return rectTransform;
        }

        public static RectTransform DrawButtonCircle(RectTransform parent)
        {
            var circle = NewCircleSprite("CircleSprite", parent);
            circle.rectTransform.sizeDelta = new Vector2(40, 40);

            var tmp = NewLabel("Text", circle.rectTransform);
            tmp.text = "A";
            tmp.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            tmp.rectTransform.anchorMin = Vector2.zero;
            tmp.rectTransform.anchorMax = Vector2.one;

            return circle.rectTransform;
        }

        public static TextMeshProUGUI NewLabel(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent);
            var text = rectTransform.AddComponent<TextMeshProUGUI>();
            text.font = ResourceDataBase.Instance.Font;
            return text;
        }

        public static ProceduralImage NewCircleSprite(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent);
            var pi = rectTransform.AddComponent<ProceduralImage>();
            rectTransform.AddComponent<RoundModifier>();
            return pi;
        }
    }
}
