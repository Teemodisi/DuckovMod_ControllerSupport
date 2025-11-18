using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MasterKeysViewOverride
    {
        protected override void Patch()
        {
            _selector = new GameObject("Selector").AddComponent<RectTransform>();
            _selector.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            _selector.SetParent(_scrollRect.content, false);
            _selector.pivot = new Vector2(0.5f, 0.5f);
            _selector.anchorMin = new Vector2(0f, 1f);
            _selector.anchorMax = new Vector2(0f, 1f);
            _selector.anchoredPosition = Vector2.zero;
            _selector.sizeDelta = _gridLayoutGroup.cellSize + Vector2.one * 5f;
            var um = _selector.gameObject.AddComponent<UniformModifier>();
            var pi = _selector.gameObject.AddComponent<ProceduralImage>();
            pi.BorderWidth = 10f;
            pi.color = UIStyle.s_Light;
            um.Radius = 20f;

            var tips = UIStyle.GamePadTipsRectTransform(transform);
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.DpadUpDownSelect,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.DpadLeftRightSelect,
                new[] { UIStyle.GamePadButton.Left, UIStyle.GamePadButton.Right });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.LeftAxisUpDownSlide,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.Exit,
                new[] { UIStyle.GamePadButton.Menu });
        }
    }
}
