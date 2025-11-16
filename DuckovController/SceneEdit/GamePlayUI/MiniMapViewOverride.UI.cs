using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MiniMapViewOverride
    {
        private ProceduralImage _iconGroupOnSelectedImage;

        private ProceduralImage _colorGroupOnSelectedImage;

        private readonly Color _selectedColor = new Color(0.25f, 0.25f, 0.25f);

        private void Patch()
        {
            var _iconRT = new GameObject("IconGroupOnSelected").AddComponent<RectTransform>();
            _iconRT.SetParent(_iconGroup.transform, false);
            _iconRT.pivot = new Vector2(0.5f, 0.5f);
            _iconRT.anchorMin = new Vector2(0f, 0.5f);
            _iconRT.anchorMax = new Vector2(1f, 0.5f);
            _iconRT.anchoredPosition = new Vector2(-100f, 0f);
            _iconRT.sizeDelta = new Vector2(300f, 30f);
            _iconRT.gameObject.AddComponent<RoundModifier>();
            _iconGroupOnSelectedImage = _iconRT.gameObject.AddComponent<ProceduralImage>();
            _iconGroupOnSelectedImage.gameObject.SetActive(false);
            _iconGroupOnSelectedImage.color = _selectedColor;
            _iconRT.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            _iconRT.SetSiblingIndex(0);

            var _colorRT = new GameObject("ColorGroupOnSelected").AddComponent<RectTransform>();
            _colorRT.SetParent(_colorGroup.transform, false);
            _colorRT.pivot = new Vector2(0.5f, 0.5f);
            _colorRT.anchorMin = new Vector2(0f, 0.5f);
            _colorRT.anchorMax = new Vector2(1f, 0.5f);
            _colorRT.anchoredPosition = new Vector2(-100f, 0f);
            _colorRT.sizeDelta = new Vector2(300f, 30f);
            _colorRT.gameObject.AddComponent<RoundModifier>();
            _colorGroupOnSelectedImage = _colorRT.gameObject.AddComponent<ProceduralImage>();
            _colorGroupOnSelectedImage.gameObject.SetActive(false);
            _colorGroupOnSelectedImage.color = _selectedColor;
            _colorRT.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            _colorRT.SetSiblingIndex(0);

            //Patch Tips
            var tipsRT = new GameObject("GamepadTips").AddComponent<RectTransform>();
            var canvasGroup = tipsRT.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            tipsRT.SetParent(transform, false);
            tipsRT.pivot = new Vector2(1f, 0f);
            tipsRT.anchorMin = new Vector2(0f, 0f);
            tipsRT.anchorMax = new Vector2(1f, 0f);
            tipsRT.anchoredPosition = new Vector2(-100f, 40f);
            tipsRT.sizeDelta = new Vector2(0, UIStyle.tips_rect_height);
            var horGroup = tipsRT.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = 10;
            horGroup.childAlignment = TextAnchor.MiddleRight;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = true;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;

            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewToolSwitchType,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewToolSelect,
                new[] { UIStyle.GamePadButton.Left, UIStyle.GamePadButton.Right });
            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewZoom,
                new[] { UIStyle.GamePadButton.LT, UIStyle.GamePadButton.RT });
            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewMoveMap,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewMoveCursor,
                new[] { UIStyle.GamePadButton.RightAxis });
            UIStyle.DrawPadButtonTips(tipsRT, L10N.Instance.MiniMapViewPinOrRemove,
                new[] { UIStyle.GamePadButton.A });
        }
    }
}
