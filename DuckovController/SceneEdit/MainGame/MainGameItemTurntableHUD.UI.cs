using Duckov.UI;
using DuckovController.Helper;
using LeTai.TrueShadow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameItemTurntableHUD
    {
        private const float table_size = 1000f;

        private const float selector_width = 50;

        private const float table_width = table_size / 6;
        private void Patch()
        {
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.anchorMin = new Vector2(0, 0);
            _rectTransform.anchorMax = new Vector2(1, 1);
            _rectTransform.anchoredPosition = new Vector2(0, 0);
            _rectTransform.sizeDelta = new Vector2(0, 0);

            var subObj = new GameObject("SubLayout");
            subObj.transform.SetParent(transform, false);
            _subTransform = subObj.AddComponent<RectTransform>();
            _subTransform.pivot = new Vector2(0.5f, 0.5f);
            _subTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _subTransform.anchorMax = new Vector2(0.5f, 0.5f);
            _subTransform.anchoredPosition = new Vector2(0, 0);
            _subTransform.sizeDelta = new Vector2(table_size, table_size);
            _canvasGroup.alpha = 0;

            subObj.AddComponent<RoundModifier>();
            var ti = subObj.AddComponent<ProceduralImage>();
            ti.BorderWidth = table_width;
            ti.color = new Color(0.016f, 0.082f, 0.132f, 0.75f);
            UIStyle.UniShadow(ti.gameObject.AddComponent<TrueShadow>());

            CreateSeparate(_subTransform, 0, "3");
            CreateSeparate(_subTransform, 60, "4");
            CreateSeparate(_subTransform, 120, "5");
            CreateSeparate(_subTransform, 180, "6");
            CreateSeparate(_subTransform, 240, "7");
            CreateSeparate(_subTransform, 300, "8");

            var selector = new GameObject("Selector");
            _selector = selector.AddComponent<RectTransform>();
            _selector.SetParent(_subTransform, false);
            _selector.pivot = new Vector2(0.5f, 0.5f);
            _selector.anchorMin = Vector2.zero;
            _selector.anchorMax = Vector2.one;
            _selector.offsetMin = Vector2.one * -selector_width;
            _selector.offsetMax = Vector2.one * selector_width;
            selector.AddComponent<RoundModifier>();
            var sit = selector.AddComponent<ProceduralImage>();
            sit.type = Image.Type.Filled;
            sit.fillOrigin = 2; //TOP
            sit.fillAmount = 1f / 6;
            sit.fillClockwise = false;
            sit.BorderWidth = table_width + selector_width;
            sit.color = new Color(0.9f, 0.9f, 0.9f, 1);
            UIStyle.UniShadow(selector.AddComponent<TrueShadow>());

            for (var i = 0; i < _turntableOptions.Length; i++)
            {
                var slot = new GameObject($"turntableOptions_{i + 3}").AddComponent<RectTransform>();
                slot.SetParent(_subTransform, false);
                _turntableOptions[i] = slot.gameObject.AddComponent<Image>();
                _turntableOptions[i].color = new Color(0.9f, 0.9f, 0.9f, 1);
                slot.pivot = Vector3.one * 0.5f;
                slot.anchorMin = Vector3.one * 0.5f;
                slot.anchorMax = Vector3.one * 0.5f;
                var angle = i * 60 * Mathf.Deg2Rad + 90;
                var dis = table_size / 2 - table_width / 2;
                slot.anchoredPosition = new Vector2(Mathf.Cos(angle) * dis, Mathf.Sin(angle) * dis);
                slot.sizeDelta = Vector2.one * (table_width - 40);
                UIStyle.UniShadow(slot.gameObject.AddComponent<TrueShadow>());
            }

            var itemPanel = FindObjectOfType<ItemShortcutPanel>(true);
            var buttons = itemPanel.GetComponentsInChildren<ItemShortcutButton>();
            if (buttons.Length != 6)
            {
                Debug.LogError($"{Utils.ModName} {nameof(MainGameItemTurntableHUD)} 快捷键数量不对齐");
            }
            for (var i = 0; i < _itemSlotMark.Length && i < buttons.Length; i++)
            {
                var rect = new GameObject($"ItemSlotMark_{i + 3}").AddComponent<RectTransform>();
                _itemSlotMark[i] = rect;
                rect.SetParent(buttons[i].transform, false);
                rect.pivot = Vector2.one * 0.5f;
                rect.anchorMin = new Vector2(0.5f, 1f);
                rect.anchorMax = new Vector2(0.5f, 1f);
                rect.anchoredPosition = new Vector2(0, 20);
                rect.sizeDelta = Vector2.one * 10;
                rect.gameObject.AddComponent<RoundModifier>();
                rect.gameObject.AddComponent<ProceduralImage>();
                UIStyle.UniShadow(rect.gameObject.AddComponent<TrueShadow>());
                _itemSlotMark[i].gameObject.SetActive(false);
            }
        }

        private static void CreateSeparate(RectTransform parent, float angle, string label)
        {
            var sep = new GameObject($"Separate_{angle}");
            var rect = sep.AddComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.pivot = new Vector2(1, 0);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(0, table_size / 2);
            rect.localEulerAngles = new Vector3(0, 0, angle);

            var subImage = new GameObject("SubImage");
            var subRect = subImage.AddComponent<RectTransform>();
            subRect.SetParent(rect, false);
            subRect.pivot = new Vector2(0.5f, 0.5f);
            subRect.anchorMin = new Vector2(0.5f, 0f);
            subRect.anchorMax = new Vector2(0.5f, 1f);
            const float len = table_size / 2 - table_width;
            subRect.anchoredPosition = new Vector2(0, len / 2);
            subRect.sizeDelta = new Vector2(4, -len);
            subImage.AddComponent<FreeModifier>();
            var image = subImage.AddComponent<ProceduralImage>();
            image.color = new Color(1, 1, 1, 0.25f);

            var tmp = UIStyle.NewLabel("SubText", parent);
            tmp.text = label;
            tmp.color = Color.white;
            tmp.horizontalAlignment = HorizontalAlignmentOptions.Center;
            tmp.verticalAlignment = VerticalAlignmentOptions.Capline;
            tmp.fontSize = 40;
            tmp.rectTransform.pivot = Vector3.one * 0.5f;
            tmp.rectTransform.anchorMin = Vector3.one * 0.5f;
            tmp.rectTransform.anchorMax = Vector3.one * 0.5f;
            var textAngle = angle * Mathf.Deg2Rad + 90;
            var textDis = len - 30;
            tmp.rectTransform.anchoredPosition = new Vector2(
                Mathf.Cos(textAngle) * textDis,
                Mathf.Sin(textAngle) * textDis);
            tmp.rectTransform.sizeDelta = Vector2.one * 50;
        }
    }
}
