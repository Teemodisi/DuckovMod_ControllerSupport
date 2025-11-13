using DG.Tweening;
using Duckov;
using Duckov.UI;
using DuckovController.Helper;
using LeTai.TrueShadow;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.MainGameInput
{
    public partial class MainGameItemTurntableHUD : SingletonMonoBehaviour<MainGameItemTurntableHUD>
    {
        private const float table_size = 1000f;

        private const float selector_width = 50;

        private const float table_width = table_size / 6;

        private RectTransform _rectTransform;

        private CanvasGroup _canvasGroup;

        private RectTransform _subTransform;

        private Tween _tween;

        private RectTransform _selector;

        private readonly Image[] _turntableOptions = new Image[6];

        private readonly RectTransform[] _itemSlotMark = new RectTransform[6];

        private InputActionMap _inputActionMap;

        private InputAction _inputAction;

        public bool Interactive { get; private set; }

        public int SelectedSlotIndex { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _rectTransform = gameObject.AddComponent<RectTransform>();
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            Patch();

            _inputActionMap = new InputActionMap("MainGameItemTurntableHUD");
            _inputAction = _inputActionMap.AddAction("AimDirection", expectedControlLayout: "Vector2");
            _inputAction.AddBinding("<Gamepad>/rightStick");
            _inputAction.performed += OnRightStickInput;

            ItemShortcut.OnSetItem += UpdateFromEvent;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ItemShortcut.OnSetItem -= UpdateFromEvent;
        }

        private void OnRightStickInput(InputAction.CallbackContext context)
        {
            if (!Interactive)
            {
                return;
            }
            var dir = context.ReadValue<Vector2>();
            if (dir.sqrMagnitude < 0.0225f)
            {
                return;
            }
            var inputAngle = Vector2.SignedAngle(Vector2.up, dir) + 30;
            if (inputAngle < 0)
            {
                inputAngle += 360;
            }
            var index = Mathf.FloorToInt(inputAngle / 60);
            if (SelectedSlotIndex != index)
            {
                SelectedSlotIndex = index;
                OnUpdateSelection();
                _selector.localRotation = Quaternion.Euler(0, 0, SelectedSlotIndex * 60);
                AudioManager.Post("UI/hover");
            }
        }

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

        private void UpdateFromEvent(int _)
        {
            UpdateSlot();
            OnUpdateSelection();
        }

        public void UpdateSlot()
        {
            for (var i = 0; i < _turntableOptions.Length; i++)
            {
                var item = ItemShortcut.Get(i);
                if (item != null)
                {
                    _turntableOptions[i].sprite = item.Icon;
                    _turntableOptions[i].color = Color.white;
                }
                else
                {
                    _turntableOptions[i].sprite = null;
                    _turntableOptions[i].color = Color.clear;
                }
            }
        }

        public void UseCurrentSlot()
        {
            RefShortCutInput(SelectedSlotIndex + 3);
        }

        private void OnUpdateSelection()
        {
            for (var i = 0; i < _itemSlotMark.Length; i++)
            {
                _itemSlotMark[i].gameObject.SetActive(SelectedSlotIndex == i);
            }
        }

        public void Show()
        {
            UpdateSlot();
            _tween?.Complete(false);
            gameObject.SetActive(true);
            Interactive = false;
            _rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);
            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1, 0.2f))
                .Insert(0, _rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack))
                .AppendCallback(() => { Interactive = true; });
            _inputActionMap.Enable();
        }

        public void Hide()
        {
            _inputActionMap.Disable();
            _tween?.Complete(false);
            Interactive = false;
            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0, 0.2f))
                .AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}
