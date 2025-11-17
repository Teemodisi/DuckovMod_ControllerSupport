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

        public static RectTransform DrawPadButtonTips(RectTransform parent, string labelText, GamePadButton[] buttons)
        {
            var rectTransform = new GameObject("GamePadTipsBtn").AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            var iconW = gamepad_btn_icon_size * buttons.Length +
                        gamepad_tips_element_interval * Mathf.Max(0, buttons.Length - 1);
            rectTransform.sizeDelta = new Vector2(gamepad_tips_label_width + iconW, 0);

            var iconContainer = new GameObject("IconContainer").AddComponent<RectTransform>();
            iconContainer.SetParent(rectTransform, false);
            iconContainer.pivot = new Vector2(0, 0.5f);
            iconContainer.anchorMin = new Vector2(0, 0);
            iconContainer.anchorMax = new Vector2(0, 1);
            iconContainer.anchoredPosition = new Vector2(0, 0);
            iconContainer.sizeDelta = new Vector2(iconW, 0);
            var horGroup = iconContainer.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = gamepad_tips_element_interval;
            horGroup.childAlignment = TextAnchor.MiddleLeft;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = true;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;

            for (var i = 0; i < buttons.Length; i++)
            {
                DrawPadButtonIcon(iconContainer, buttons[i]);
            }

            var label = NewLabel("Label", rectTransform);
            label.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            label.rectTransform.anchorMin = Vector2.zero;
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.offsetMin = new Vector2(iconW + gamepad_tips_element_interval, 0);
            label.rectTransform.offsetMax = new Vector2(-gamepad_tips_element_interval, 0);
            label.color = s_Light;
            label.horizontalAlignment = HorizontalAlignmentOptions.Left;
            label.verticalAlignment = VerticalAlignmentOptions.Capline;
            label.fontSize = gamepad_tips_label_size;
            label.text = labelText;
            label.fontStyle = FontStyles.Bold;
            return rectTransform;
        }

        public static RectTransform GamePadTipsRectTransform(Transform parent)
        {
            var tipsRT = new GameObject("GamepadTips").AddComponent<RectTransform>();
            var canvasGroup = tipsRT.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            tipsRT.SetParent(parent, false);
            tipsRT.pivot = new Vector2(1f, 0f);
            tipsRT.anchorMin = new Vector2(0f, 0f);
            tipsRT.anchorMax = new Vector2(1f, 0f);
            tipsRT.anchoredPosition = new Vector2(-100f, 50f);
            tipsRT.sizeDelta = new Vector2(0, gamepad_tips_rect_height);
            var horGroup = tipsRT.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = 10;
            horGroup.childAlignment = TextAnchor.MiddleRight;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = true;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;
            return tipsRT;
        }

    #region GamePadButtonType

        public const int gamepad_btn_icon_size = 30;

        public const int gamepad_tips_rect_height = gamepad_btn_icon_size;

        public const int gamepad_icon_dpad_thickness = 8;

        public const int gamepad_icon_radius = 3;

        public const int gamepad_tips_label_width = 150;

        public const int gamepad_tips_element_interval = 10;

        public const int gamepad_tips_label_size = 24;

        public const int gamepad_btn_icon_text_size = gamepad_tips_label_size - 3;

        public static readonly Color s_Light = new Color(0.95f, 0.95f, 0.95f, 1f);

        public static readonly Color s_Gray = new Color(0.5f, 0.5f, 0.5f, 1f);

        public static readonly Color s_Dark = new Color(0.2f, 0.2f, 0.2f, 1f);

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
                    return DrawPadAxisButton(parent, button);
                case GamePadButton.LeftAxisPress:
                case GamePadButton.RightAxisPress:
                    return DrawPadAxisPressButton(parent, button);
                case GamePadButton.LB:
                case GamePadButton.RB:
                    return DrawPadShoulderButton(parent, button);
                case GamePadButton.LT:
                case GamePadButton.RT:
                    return DrawPadTriggerButton(parent, button);
                case GamePadButton.Menu:
                    return DrawPadMenuButton(parent, button);
                case GamePadButton.Select:
                    return DrawPadSelectButton(parent, button);
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
            container.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);

            var underHor = NewRoundRectSprite("underHor", container, gamepad_icon_radius);
            underHor.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            underHor.rectTransform.anchorMin = new Vector2(0, 0.5f);
            underHor.rectTransform.anchorMax = new Vector2(1, 0.5f);
            underHor.rectTransform.anchoredPosition = Vector2.zero;
            underHor.rectTransform.sizeDelta = new Vector2(0, gamepad_icon_dpad_thickness - 1);
            UniShadow(underHor.gameObject.AddComponent<TrueShadow>());

            var underVer = NewRoundRectSprite("underVer", container, gamepad_icon_radius);
            underVer.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            underVer.rectTransform.anchorMin = new Vector2(0.5f, 0);
            underVer.rectTransform.anchorMax = new Vector2(0.5f, 1);
            underVer.rectTransform.anchoredPosition = Vector2.zero;
            underVer.rectTransform.sizeDelta = new Vector2(gamepad_icon_dpad_thickness - 1, 0);
            UniShadow(underVer.gameObject.AddComponent<TrueShadow>());

            var left = NewRoundRectSprite("left", container, gamepad_icon_radius);
            left.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            left.rectTransform.anchorMin = new Vector2(0, 0.5f);
            left.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            left.rectTransform.anchoredPosition = Vector2.zero;
            left.rectTransform.sizeDelta = new Vector2(0, gamepad_icon_dpad_thickness);

            var right = NewRoundRectSprite("right", container, gamepad_icon_radius);
            right.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            right.rectTransform.anchorMax = new Vector2(1, 0.5f);
            right.rectTransform.anchoredPosition = Vector2.zero;
            right.rectTransform.sizeDelta = new Vector2(0, gamepad_icon_dpad_thickness);

            var up = NewRoundRectSprite("up", container, gamepad_icon_radius);
            up.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            up.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            up.rectTransform.anchorMax = new Vector2(0.5f, 1);
            up.rectTransform.anchoredPosition = Vector2.zero;
            up.rectTransform.sizeDelta = new Vector2(gamepad_icon_dpad_thickness, 0);

            var down = NewRoundRectSprite("down", container, gamepad_icon_radius);
            down.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            down.rectTransform.anchorMin = new Vector2(0.5f, 0);
            down.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            down.rectTransform.anchoredPosition = Vector2.zero;
            down.rectTransform.sizeDelta = new Vector2(gamepad_icon_dpad_thickness, 0);

            var cover = NewRoundRectSprite("cover", container, 0);
            cover.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cover.rectTransform.anchoredPosition = Vector2.zero;
            cover.rectTransform.sizeDelta = new Vector2(gamepad_icon_dpad_thickness, gamepad_icon_dpad_thickness);
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
            circle.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);
            UniShadow(circle.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", circle.rectTransform);
            TipsTextOnBtnProcess(btnLabel);
            switch (button)
            {
                case GamePadButton.A: btnLabel.text = "A"; break;
                case GamePadButton.B: btnLabel.text = "B"; break;
                case GamePadButton.X: btnLabel.text = "X"; break;
                case GamePadButton.Y: btnLabel.text = "Y"; break;
                default: btnLabel.text = ""; break;
            }
            return circle.rectTransform;
        }

        private static RectTransform DrawPadAxisButton(RectTransform parent, GamePadButton button)
        {
            var circle = NewCircleSprite("CircleSprite", parent);
            circle.rectTransform.pivot = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMin = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMax = new Vector2(0, 0.5f);
            circle.rectTransform.anchoredPosition = Vector2.zero;
            circle.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);
            circle.BorderWidth = 2;
            UniShadow(circle.gameObject.AddComponent<TrueShadow>());

            var subCircle = NewCircleSprite("SubCircleSprite", circle.rectTransform);
            subCircle.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            subCircle.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            subCircle.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            subCircle.rectTransform.anchoredPosition = Vector2.zero;
            subCircle.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size - 8, gamepad_btn_icon_size - 8);
            UniShadow(subCircle.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", circle.rectTransform);
            TipsTextOnBtnProcess(btnLabel);
            btnLabel.text = button == GamePadButton.LeftAxis ? "L" : "R";
            return circle.rectTransform;
        }

        private static RectTransform DrawPadAxisPressButton(RectTransform parent, GamePadButton button)
        {
            var container = new GameObject("SprContainer").AddComponent<RectTransform>();
            container.SetParent(parent, false);
            container.pivot = new Vector2(0, 0.5f);
            container.anchorMin = new Vector2(0, 0.5f);
            container.anchorMax = new Vector2(0, 0.5f);
            container.anchoredPosition = Vector2.zero;
            container.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);

            var spr1 = new GameObject("Sprite1").AddComponent<RectTransform>();
            spr1.SetParent(container, false);
            spr1.pivot = new Vector2(0.5f, 0.5f);
            spr1.anchorMin = new Vector2(0.5f, 0f);
            spr1.anchorMax = new Vector2(0.5f, 1f);
            spr1.anchoredPosition = Vector2.zero;
            spr1.sizeDelta = new Vector2(18f, 0f);
            var um1 = spr1.gameObject.AddComponent<UniformModifier>();
            spr1.gameObject.AddComponent<ProceduralImage>().color = s_Light;
            um1.Radius = gamepad_icon_radius;
            UniShadow(spr1.gameObject.AddComponent<TrueShadow>());

            var spr2 = new GameObject("Sprite2").AddComponent<RectTransform>();
            spr2.SetParent(container, false);
            spr2.pivot = new Vector2(0.5f, 1f);
            spr2.anchorMin = new Vector2(0f, 1f);
            spr2.anchorMax = new Vector2(1f, 1f);
            spr2.anchoredPosition = Vector2.zero;
            spr2.sizeDelta = new Vector2(0, 10f);
            var um2 = spr2.gameObject.AddComponent<UniformModifier>();
            spr2.gameObject.AddComponent<ProceduralImage>().color = s_Light;
            um2.Radius = gamepad_icon_radius;
            UniShadow(spr2.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", container);
            TipsTextOnBtnProcess(btnLabel);
            btnLabel.text = button == GamePadButton.LeftAxisPress ? "L" : "R";

            return container;
        }

        private static RectTransform DrawPadShoulderButton(RectTransform parent, GamePadButton button)
        {
            var isLeft = button == GamePadButton.LB;
            var (image, modifier) = NewFreeRectSprite("RoundSprite", parent);
            image.rectTransform.pivot = new Vector2(0, 0.5f);
            image.rectTransform.anchorMin = new Vector2(0, 0.5f);
            image.rectTransform.anchorMax = new Vector2(0, 0.5f);
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);
            var r = gamepad_icon_radius;
            modifier.Radius = isLeft
                ? new Vector4(gamepad_btn_icon_size * 0.3f, r, r, r)
                : new Vector4(r, gamepad_btn_icon_size * 0.3f, r, r);
            UniShadow(image.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", image.rectTransform);
            TipsTextOnBtnProcess(btnLabel);
            btnLabel.characterSpacing = -8;
            btnLabel.text = isLeft ? "LB" : "RB";
            return image.rectTransform;
        }

        private static RectTransform DrawPadTriggerButton(RectTransform parent, GamePadButton button)
        {
            var container = new GameObject("SprContainer").AddComponent<RectTransform>();
            container.SetParent(parent, false);
            container.pivot = new Vector2(0, 0.5f);
            container.anchorMin = new Vector2(0, 0.5f);
            container.anchorMax = new Vector2(0, 0.5f);
            container.anchoredPosition = Vector2.zero;
            container.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);

            var spr1 = new GameObject("Sprite1").AddComponent<RectTransform>();
            spr1.SetParent(container, false);
            spr1.pivot = new Vector2(0.5f, 0.5f);
            spr1.anchorMin = Vector2.zero;
            spr1.anchorMax = Vector2.one;
            spr1.offsetMin = new Vector2(0, 5);
            spr1.offsetMax = Vector2.zero;
            var um = spr1.gameObject.AddComponent<UniformModifier>();
            spr1.gameObject.AddComponent<ProceduralImage>().color = s_Light;
            um.Radius = gamepad_icon_radius;
            UniShadow(spr1.gameObject.AddComponent<TrueShadow>());

            var spr2 = new GameObject("Sprite2").AddComponent<RectTransform>();
            spr2.SetParent(container, false);
            spr2.pivot = new Vector2(0.5f, 0.5f);
            spr2.anchorMin = Vector2.zero;
            spr2.anchorMax = Vector2.one;
            var fm = spr2.gameObject.AddComponent<FreeModifier>();
            spr2.gameObject.AddComponent<ProceduralImage>().color = s_Light;
            UniShadow(spr2.gameObject.AddComponent<TrueShadow>());

            var btnLabel = NewLabel("Text", container);
            TipsTextOnBtnProcess(btnLabel);
            btnLabel.characterSpacing = -8;
            if (button == GamePadButton.LT)
            {
                fm.Radius = new Vector4(gamepad_icon_radius, gamepad_icon_radius, 10, gamepad_icon_radius);
                spr2.offsetMin = Vector2.zero;
                spr2.offsetMax = new Vector2(-10, 0);
                btnLabel.text = "LT";
            }
            else
            {
                fm.Radius = new Vector4(gamepad_icon_radius, gamepad_icon_radius, gamepad_icon_radius, 10);
                spr2.offsetMin = new Vector2(10, 0);
                spr2.offsetMax = Vector2.zero;
                btnLabel.text = "RT";
            }
            return container;
        }

        private static RectTransform DrawPadMenuButton(RectTransform parent, GamePadButton button)
        {
            var circle = NewCircleSprite("CircleSprite", parent);
            circle.rectTransform.pivot = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMin = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMax = new Vector2(0, 0.5f);
            circle.rectTransform.anchoredPosition = Vector2.zero;
            circle.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);
            circle.BorderWidth = 2;
            UniShadow(circle.gameObject.AddComponent<TrueShadow>());

            var cube1 = NewRoundRectSprite("Cube1", circle.rectTransform, 0);
            cube1.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchoredPosition = new Vector2(0, 4);
            cube1.rectTransform.sizeDelta = new Vector2(14, 2);
            UniShadow(cube1.gameObject.AddComponent<TrueShadow>());

            var cube2 = NewRoundRectSprite("Cube2", circle.rectTransform, 0);
            cube2.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchoredPosition = new Vector2(0, 0);
            cube2.rectTransform.sizeDelta = new Vector2(14, 2);
            UniShadow(cube2.gameObject.AddComponent<TrueShadow>());

            var cube3 = NewRoundRectSprite("Cube3", circle.rectTransform, 0);
            cube3.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cube3.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cube3.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cube3.rectTransform.anchoredPosition = new Vector2(0, -4);
            cube3.rectTransform.sizeDelta = new Vector2(14, 2);
            UniShadow(cube3.gameObject.AddComponent<TrueShadow>());

            return circle.rectTransform;
        }

        private static RectTransform DrawPadSelectButton(RectTransform parent, GamePadButton button)
        {
            var circle = NewCircleSprite("CircleSprite", parent);
            circle.rectTransform.pivot = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMin = new Vector2(0, 0.5f);
            circle.rectTransform.anchorMax = new Vector2(0, 0.5f);
            circle.rectTransform.anchoredPosition = Vector2.zero;
            circle.rectTransform.sizeDelta = new Vector2(gamepad_btn_icon_size, gamepad_btn_icon_size);
            circle.BorderWidth = 2;
            UniShadow(circle.gameObject.AddComponent<TrueShadow>());

            var cube1 = NewRoundRectSprite("Cube1", circle.rectTransform, 0);
            cube1.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cube1.rectTransform.anchoredPosition = new Vector2(-2, 2);
            cube1.rectTransform.sizeDelta = new Vector2(12, 8);
            cube1.type = Image.Type.Filled;
            cube1.fillMethod = Image.FillMethod.Radial360;
            cube1.fillAmount = 0.75f;
            cube1.fillOrigin = 0;
            cube1.BorderWidth = 2;
            UniShadow(cube1.gameObject.AddComponent<TrueShadow>());

            var cube2 = NewRoundRectSprite("Cube2", circle.rectTransform, 0);
            cube2.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cube2.rectTransform.anchoredPosition = new Vector2(2, -2);
            cube2.rectTransform.sizeDelta = new Vector2(12, 8);
            cube2.BorderWidth = 2;
            UniShadow(cube2.gameObject.AddComponent<TrueShadow>());

            return circle.rectTransform;
        }

        private static void TipsTextOnBtnProcess(TextMeshProUGUI label)
        {
            label.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            label.rectTransform.anchorMin = Vector2.zero;
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.anchoredPosition = Vector2.zero;
            label.rectTransform.sizeDelta = Vector2.one * -10; // Offset
            label.color = s_Dark;
            label.horizontalAlignment = HorizontalAlignmentOptions.Center;
            label.verticalAlignment = VerticalAlignmentOptions.Capline;
            label.fontSize = gamepad_btn_icon_text_size;
            label.fontStyle = FontStyles.Bold;
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
            rectTransform.gameObject.AddComponent<RoundModifier>();
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
            pi.color = s_Light;
            return pi;
        }

        public static ProceduralImage NewRoundRectSprite(string name, RectTransform parent, float radius)
        {
            var rectTransform = new GameObject(name).AddComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            var um = rectTransform.gameObject.AddComponent<UniformModifier>();
            var pi = rectTransform.gameObject.AddComponent<ProceduralImage>();
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

            LeftAxisPress,

            RightAxisPress,

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
