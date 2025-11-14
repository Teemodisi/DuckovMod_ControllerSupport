using DG.Tweening;
using Duckov;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameItemTurntableHUD : SingletonMonoBehaviour<MainGameItemTurntableHUD>
    {
        private RectTransform _rectTransform;

        private CanvasGroup _canvasGroup;

        private RectTransform _subTransform;

        private Tween _tween;

        private RectTransform _selector;

        private readonly Image[] _turntableOptions = new Image[6];

        private readonly RectTransform[] _itemSlotMark = new RectTransform[6];

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
            InitInputAction();
        }

        private void OnEnable()
        {
            _inputAction.Enable();
            ItemShortcut.OnSetItem += UpdateFromEvent;
        }

        private void OnDisable()
        {
            _inputAction.Disable();
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
        }

        public void Hide()
        {
            _inputAction.Disable();
            _tween?.Complete(false);
            Interactive = false;
            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0, 0.2f))
                .AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}
