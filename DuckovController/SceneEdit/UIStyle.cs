using LeTai.TrueShadow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit
{
    public static class UIStyle
    {
        public static TMP_FontAsset currentFont = null;

        private static Material s_TranslucentImageMaterial;

        public static Material TranslucentImageMaterial
        {
            get
            {
                if (s_TranslucentImageMaterial == null)
                {
                    s_TranslucentImageMaterial = new Material(Shader.Find("UI/TranslucentImage"));
                }

                return s_TranslucentImageMaterial;
            }
        }

        public static RectTransform DrawPadButtonTips(RectTransform parent, GamePadButton button, string labelText)
        {
            var rectTransform = new GameObject("GamePadTips").AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.sizeDelta = new Vector2(160, icon_size);

            var icon = DrawPadButtonIcon(rectTransform, button);

            var label = NewLabel("Label", rectTransform);
            label.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            label.rectTransform.anchorMin = Vector2.zero;
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.anchoredPosition = new Vector2(20, 0);
            label.rectTransform.sizeDelta = new Vector2(-60, 0);
            label.color = Color.white;
            label.horizontalAlignment = HorizontalAlignmentOptions.Left;
            label.verticalAlignment = VerticalAlignmentOptions.Capline;
            label.fontSize = game_pad_button_label_size;
            label.text = labelText;
            label.fontStyle = FontStyles.Bold;

            return rectTransform;
        }

    #region GamePadButtonType

        private const int icon_size = 40;

        private const int dpad_thickness = 8;

        private const int rect_radius = 3;

        private static readonly Color s_Light = new Color(0.95f, 0.95f, 0.95f, 1f);

        private static readonly Color s_Gray = new Color(0.5f, 0.5f, 0.5f, 1f);

        private static readonly Color s_Dark = new Color(0.2f, 0.2f, 0.2f, 1f);

        private const int game_pad_button_label_size = 24;

        private const int game_pad_button_size = game_pad_button_label_size - 3;

        public static RectTransform DrawPadButtonIcon(RectTransform parent, GamePadButton button)
        {
            switch (button)
            {
                case GamePadButton.A:
                case GamePadButton.B:
                case GamePadButton.X:
                case GamePadButton.Y:
                    return DrawPadABXYButton(parent, button);
                case GamePadButton.Up:
                case GamePadButton.Down:
                case GamePadButton.Left:
                case GamePadButton.Right:
                    return DrawPadDPadButton(parent, button);
                case GamePadButton.LeftAxis:
                case GamePadButton.RightAxis:
                case GamePadButton.LeftAxisBtn:
                case GamePadButton.RightAxisBtn:
                    //TODO:
                    return null;
                case GamePadButton.LB:
                case GamePadButton.RB:
                    return DrawPadShoulderButton(parent, button);
                case GamePadButton.LT:
                case GamePadButton.RT:
                    //TODO:
                    return null;
                case GamePadButton.Menu:
                    //TODO:
                    return null;
                case GamePadButton.Select:
                    //TODO:
                    return null;
            }
            return null;
        }

        private static RectTransform DrawPadDPadButton(RectTransform parent, GamePadButton button)
        {
            var container = new GameObject("rectContainer").AddComponent<RectTransform>();
            container.SetParent(parent, false);
            container.pivot = new Vector2(0, 0.5f);
            container.anchorMin = new Vector2(0, 0.5f);
            container.anchorMax = new Vector2(0, 0.5f);
            container.anchoredPosition = Vector2.zero;
            container.sizeDelta = new Vector2(icon_size, icon_size);

            var underHor = NewRoundRectSprite("underHor", container, rect_radius);
            underHor.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            underHor.rectTransform.anchorMin = new Vector2(0, 0.5f);
            underHor.rectTransform.anchorMax = new Vector2(1, 0.5f);
            underHor.rectTransform.anchoredPosition = Vector2.zero;
            underHor.rectTransform.sizeDelta = new Vector2(0, dpad_thickness - 1);
            UniShadow(underHor.gameObject.AddComponent<TrueShadow>());

            var underVer = NewRoundRectSprite("underVer", container, rect_radius);
            underVer.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            underVer.rectTransform.anchorMin = new Vector2(0.5f, 0);
            underVer.rectTransform.anchorMax = new Vector2(0.5f, 1);
            underVer.rectTransform.anchoredPosition = Vector2.zero;
            underVer.rectTransform.sizeDelta = new Vector2(dpad_thickness - 1, 0);
            UniShadow(underVer.gameObject.AddComponent<TrueShadow>());

            var left = NewRoundRectSprite("left", container, rect_radius);
            left.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            left.rectTransform.anchorMin = new Vector2(0, 0.5f);
            left.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            left.rectTransform.anchoredPosition = Vector2.zero;
            left.rectTransform.sizeDelta = new Vector2(0, dpad_thickness);

            var right = NewRoundRectSprite("right", container, rect_radius);
            right.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            right.rectTransform.anchorMax = new Vector2(1, 0.5f);
            right.rectTransform.anchoredPosition = Vector2.zero;
            right.rectTransform.sizeDelta = new Vector2(0, dpad_thickness);

            var up = NewRoundRectSprite("up", container, rect_radius);
            up.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            up.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            up.rectTransform.anchorMax = new Vector2(0.5f, 1);
            up.rectTransform.anchoredPosition = Vector2.zero;
            up.rectTransform.sizeDelta = new Vector2(dpad_thickness, 0);

            var down = NewRoundRectSprite("down", container, rect_radius);
            down.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            down.rectTransform.anchorMin = new Vector2(0.5f, 0);
            down.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            down.rectTransform.anchoredPosition = Vector2.zero;
            down.rectTransform.sizeDelta = new Vector2(dpad_thickness, 0);

            var cover = NewRoundRectSprite("cover", container, 0);
            cover.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchoredPosition = Vector2.zero;
            cover.rectTransform.sizeDelta = new Vector2(dpad_thickness, dpad_thickness);
            switch (button)
            {
                case GamePadButton.Up:
                    up.color = s_Gray;
                    break;
                case GamePadButton.Down:
                    down.color = s_Gray;
                    break;
                case GamePadButton.Left:
                    left.color = s_Gray;
                    break;
                case GamePadButton.Right:
                    right.color = s_Gray;
                    break;
            }
            return container;
        }

        private static RectTransform DrawPadABXYButton(RectTransform parent, GamePadButton button)
        {
            var circle = NewCircleSprite("CircleSprite", parent);
            circle.rectTransform.pivot = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMin = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMax = new Vector2(0, 0.5f);
            circle.rectTransform.anchoredPosition = Vector2.zero;
            circle.rectTransform.sizeDelta = new Vector2(icon_size, icon_size);
            UniShadow(circle.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", circle.rectTransform);
            btnLabel.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            btnLabel.rectTransform.anchorMin = Vector2.zero;
            btnLabel.rectTransform.anchorMax = Vector2.one;
            btnLabel.rectTransform.anchoredPosition = Vector2.zero;
            btnLabel.rectTransform.sizeDelta = Vector2.zero;
            btnLabel.color = s_Dark;
            btnLabel.horizontalAlignment = HorizontalAlignmentOptions.Center;
            btnLabel.verticalAlignment = VerticalAlignmentOptions.Capline;
            btnLabel.fontSize = game_pad_button_size;
            switch (button)
            {
                case GamePadButton.A: btnLabel.text = "A"; break;
                case GamePadButton.B: btnLabel.text = "B"; break;
                case GamePadButton.X: btnLabel.text = "X"; break;
                case GamePadButton.Y: btnLabel.text = "Y"; break;
                default: btnLabel.text = ""; break;
            }
            btnLabel.fontStyle = FontStyles.Bold;
            return circle.rectTransform;
        }

        private static RectTransform DrawPadShoulderButton(RectTransform parent, GamePadButton button)
        {
            var isLeft = button == GamePadButton.LB;
            var (image, modifier) = NewFreeRectSprite("RoundSprite", parent);
            image.rectTransform.pivot = new Vector2(0, 0.5f);
            image.rectTransform.anchorMin = new Vector2(0, 0.5f);
            image.rectTransform.anchorMax = new Vector2(0, 0.5f);
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.sizeDelta = new Vector2(icon_size, icon_size);
            var r = rect_radius;
            modifier.Radius = isLeft
                ? new Vector4(icon_size * 0.3f, r, r, r)
                : new Vector4(r, icon_size * 0.3f, r, r);
            UniShadow(image.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", image.rectTransform);
            btnLabel.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            btnLabel.rectTransform.anchorMin = Vector2.zero;
            btnLabel.rectTransform.anchorMax = Vector2.one;
            btnLabel.rectTransform.anchoredPosition = Vector2.zero;
            btnLabel.rectTransform.sizeDelta = Vector2.one * -10; // Offset
            btnLabel.color = s_Dark;
            btnLabel.horizontalAlignment = HorizontalAlignmentOptions.Center;
            btnLabel.verticalAlignment = VerticalAlignmentOptions.Capline;
            btnLabel.fontSize = game_pad_button_size;
            btnLabel.text = isLeft ? "LB" : "RB";
            btnLabel.fontStyle = FontStyles.Bold;
            return image.rectTransform;
        }

    #endregion

    #region UGUI

        public static TextMeshProUGUI NewLabel(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            var text = rectTransform.gameObject.AddComponent<TextMeshProUGUI>();
            if (currentFont != null)
            {
                text.font = currentFont;
            }
            text.color = s_Dark;
            UniShadow(rectTransform.gameObject.AddComponent<TrueShadow>());
            return text;
        }

        public static ProceduralImage NewCircleSprite(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
            rectTransform.gameObject.AddComponent<RoundModifier>();
            pi.color = s_Light;
            return pi;
        }

        public static ProceduralImage NewRoundRectSprite(string name, RectTransform parent, float radius)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
            var um = rectTransform.gameObject.AddComponent<UniformModifier>();
            pi.color = s_Light;
            um.Radius = radius;
            return pi;
        }

        public static (ProceduralImage, FreeModifier) NewFreeRectSprite(string name, RectTransform parent)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            var um = rectTransform.gameObject.AddComponent<FreeModifier>();
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
            pi.color = s_Light;
            return (pi, um);
        }

        public static Image DebugRect(this RectTransform parent, Color color)
        {
            var rectTransform = new GameObject("DebugRect").AddComponent<RectTransform>();
            var element = rectTransform.gameObject.AddComponent<LayoutElement>();
            element.ignoreLayout = true;
            rectTransform.SetParent(parent, false);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = Vector2.zero;
            var image = rectTransform.gameObject.AddComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        public static void UniShadow(TrueShadow shadow)
        {
            //统一阴影样式
            shadow.Size = 10;
            shadow.Spread = 0;
            shadow.UseGlobalAngle = false;
            shadow.OffsetAngle = 30.7f;
            shadow.OffsetDistance = 9.8f;
            shadow.Color = new Color(0, 0, 0, 0.510f);
            shadow.Inset = false;
            shadow.BlendMode = BlendMode.Normal;
        }

    #endregion

    #region Enum

        public enum GamePadButton
        {
            A,

            B,

            X,

            Y,

            Up,

            Down,

            Left,

            Right,

            LeftAxis,

            RightAxis,

            LeftAxisBtn,

            RightAxisBtn,

            LB,

            RB,

            LT,

            RT,

            Menu,

            Select
        }

        public enum GamePadButtonStyle
        {
            ABXY,

            DPadUp,

            DPadDown,

            DPadLeft,

            DPadRight,

            Axis,

            Trigger, //LT,RT

            ShoulderButton, //LB, RB

            Menu,

            Select
        }

    #endregion
    }
}
