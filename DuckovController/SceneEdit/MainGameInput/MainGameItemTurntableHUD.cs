using DG.Tweening;
using Duckov;
using DuckovController.Helper;
using LeTai.TrueShadow;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.MainGameInput
{
    public class MainGameItemTurntableHUD : SingletonMonoBehaviour<MainGameItemTurntableHUD>
    {
        private const float table_size = 1000f;

        private const float selector_width = 50;

        private const float table_width = table_size / 6;

        private RectTransform _rectTransform;

        private CanvasGroup _canvasGroup;

        private RectTransform _subTransform;

        private Tween _tween;

        private RectTransform _selector;

        private RectTransform[] _slot = new RectTransform[6];

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
            var inputAngle = Vector2.SignedAngle(Vector2.up, dir);
            var index = Mathf.FloorToInt((inputAngle + 30) / 60);
            if (SelectedSlotIndex != index)
            {
                SelectedSlotIndex = index;
                _rectTransform.localRotation = Quaternion.Euler(0, 0, SelectedSlotIndex * 60);
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
            UIStyle.UniShadow(ti.AddComponent<TrueShadow>());

            CreateSeparate(_subTransform, 0);
            CreateSeparate(_subTransform, 60);
            CreateSeparate(_subTransform, 120);
            CreateSeparate(_subTransform, 180);
            CreateSeparate(_subTransform, 240);
            CreateSeparate(_subTransform, 300);

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
        }

        private static void CreateSeparate(RectTransform parent, float angle)
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
            var len = table_size / 2 - table_width;
            subRect.anchoredPosition = new Vector2(0, len / 2);
            subRect.sizeDelta = new Vector2(4, -len);
            subImage.AddComponent<FreeModifier>();
            var image = subImage.AddComponent<ProceduralImage>();
            image.color = new Color(1, 1, 1, 0.25f);
        }

        public void Show()
        {
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
