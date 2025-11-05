using DuckovController.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit
{
    public static class UIStyle
    {
        public static RectTransform DrawPadTips(RectTransform parent)
        {
            var rectTransform = new GameObject("GamePadTips").AddComponent<RectTransform>();
            rectTransform.SetParent(parent,false);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.sizeDelta = new Vector2(160, 40);
            
            var circle = NewCircleSprite("CircleSprite", rectTransform);
            circle.rectTransform.pivot = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMin = new Vector2(0, 0);
            circle.rectTransform.anchorMax = new Vector2(0, 1);
            circle.rectTransform.anchoredPosition = Vector2.zero;
            circle.rectTransform.sizeDelta = new Vector2(40, 0);

            var btnLabel = NewLabel("Text", circle.rectTransform);
            btnLabel.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            btnLabel.rectTransform.anchorMin = Vector2.zero;
            btnLabel.rectTransform.anchorMax = Vector2.one;
            btnLabel.rectTransform.anchoredPosition = Vector2.zero;
            btnLabel.rectTransform.sizeDelta = Vector2.zero;
            btnLabel.color = Color.black;
            btnLabel.horizontalAlignment = HorizontalAlignmentOptions.Center;
            btnLabel.verticalAlignment = VerticalAlignmentOptions.Middle;
            btnLabel.fontSize = 25;
            btnLabel.text = "A";
            btnLabel.fontStyle = FontStyles.Bold;

            var label = NewLabel("Label", rectTransform);
            label.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            label.rectTransform.anchorMin = Vector2.zero;
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.anchoredPosition = new Vector2(20, 0);
            label.rectTransform.sizeDelta = new Vector2(-60, 0);
            label.color = Color.white;
            label.horizontalAlignment = HorizontalAlignmentOptions.Left;
            label.verticalAlignment = VerticalAlignmentOptions.Middle;
            label.fontSize = 30;
            label.text = "你好";
            label.fontStyle = FontStyles.Bold;

            return rectTransform;
        }

        public static TextMeshProUGUI NewLabel(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent,false);
            var text = rectTransform.gameObject.AddComponent<TextMeshProUGUI>();
            text.font = ResourceDataBase.Instance.Font;
            return text;
        }

        public static ProceduralImage NewCircleSprite(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent,false);
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
            rectTransform.gameObject.AddComponent<RoundModifier>();
            return pi;
        }

        public static Image DebugRect(RectTransform parent, Color color)
        {
            var rectTransform = new GameObject("DebugRect").AddComponent<RectTransform>();
            var element = rectTransform.gameObject.AddComponent<LayoutElement>();
            element.ignoreLayout = true;
            rectTransform.SetParent(parent,false);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = Vector2.zero;
            var image = rectTransform.gameObject.AddComponent<Image>();
            image.color = color;
            return image;
        }
    }
}
