using Duckov.MasterKeys.UI;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MasterKeysViewOverride : AbstractPatch
    {
        private MasterKeysView _view;

        private ScrollRect _scrollRect;

        private GridLayoutGroup _gridLayoutGroup;

        private RectTransform _selector;

        private ScrollViewGamepadControl _scrollViewGamepadControl;

        private MasterKeysIndexEntry[] _selections;

        private int _selectionIndex = -1;

        protected override void Awake()
        {
            _view = gameObject.GetComponent<MasterKeysView>();
            _scrollRect = transform.FindWithDebug("Content/Content/Scroll View").GetComponent<ScrollRect>();
            _scrollViewGamepadControl = _scrollRect.gameObject.AddComponent<ScrollViewGamepadControl>();
            _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _selector.gameObject.SetActive(false);
            SelectWithOffset(0);
        }

        private void OnNavigationInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var value = context.ReadValue<Vector2>();
                if (value.x > 0.1f)
                {
                    SelectWithOffset(1);
                }
                else if (value.x < -0.1f)
                {
                    SelectWithOffset(-1);
                }
                else if (value.y > 0.1f)
                {
                    SelectWithOffset(-10);
                }
                else if (value.y < -0.1f)
                {
                    SelectWithOffset(10);
                }
            }
        }

        private void SelectWithOffset(int offsetIndex)
        {
            UpdateSelections();
            if (_selections.Length <= 0)
            {
                return;
            }

            var w = _scrollRect.content.rect.width;
            var spaceX = _gridLayoutGroup.spacing.x;
            var col = Mathf.FloorToInt((w - spaceX) / (_gridLayoutGroup.cellSize.x + spaceX));
            
            var originIndex = _selectionIndex;
            var index = originIndex;
            var originRow = Mathf.FloorToInt((float)index / col);
#if DEBUG
            Debug.Log($"GetSelectIndex{index} originRow{originRow} col {col}");
#endif
            if (index < 0)
            {
                OnSelect(0);
                return;
            }
            index += offsetIndex;
            if (index < 0 || index >= _selections.Length)
            {
#if DEBUG
                Debug.Log($"过界 To{index}");
#endif
                OnSelect(originIndex);
                return;
            }
            var nowRow = Mathf.FloorToInt((float)index / col);
            if (Mathf.Abs(offsetIndex) < col && originRow != nowRow)
            {
#if DEBUG
                Debug.Log($"过行 To{index}  Row{nowRow} != {originRow}");
#endif
                OnSelect(originIndex);
                return;
            }
            OnSelect(index);
            return;

            void OnSelect(int i)
            {
                var target = _selections[i];
                target.OnPointerClick(new PointerEventData(EventSystem.current));
                _selector.gameObject.SetActive(true);
                _selector.anchoredPosition = target.GetComponent<RectTransform>().anchoredPosition;
                _selectionIndex = i;
            }
        }

        private void UpdateSelections()
        {
            _selections = _gridLayoutGroup.GetComponentsInChildren<MasterKeysIndexEntry>();
        }

        private void OnScrollInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _scrollViewGamepadControl.Move(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                _scrollViewGamepadControl.Move(Vector2.zero);
            }
        }
    }
}
