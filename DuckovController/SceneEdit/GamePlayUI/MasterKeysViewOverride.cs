using Duckov.MasterKeys.UI;
using Duckov.UI.Animations;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
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

        private FadeGroup _fadeGroup;

        private ScrollViewGamepadControl _scrollViewGamepadControl;

        private GridSelectGroup<MasterKeysIndexEntry> _gridSelectGroup;

        protected override void Awake()
        {
            _view = gameObject.GetComponent<MasterKeysView>();
            _scrollRect = transform.FindWithDebug("Content/Content/Scroll View").GetComponent<ScrollRect>();
            _scrollViewGamepadControl = _scrollRect.gameObject.AddComponent<ScrollViewGamepadControl>();
            _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
            _fadeGroup = gameObject.GetComponent<FadeGroup>();
            _fadeGroup.OnShowComplete += _ =>
            {
                _gridSelectGroup.UpdateSelections();
                if (_gridSelectGroup.CurrentSelection == null)
                {
                    _gridSelectGroup.Select(0);
                }
            };

            base.Awake();

            _gridSelectGroup = new GridSelectGroup<MasterKeysIndexEntry>(
                _gridLayoutGroup,
                () => _gridLayoutGroup.GetComponentsInChildren<MasterKeysIndexEntry>(),
                (item, index) =>
                {
                    item.gameObject.EmitEventPointerClickAndDownBtnLeft();
                    _selector.gameObject.SetActive(true);
                    _selector.anchoredPosition = item.GetComponent<RectTransform>().anchoredPosition;
                }
            );
        }

        private void OnNavigationInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var value = context.ReadValue<Vector2>();
                if (value.x > 0.1f)
                {
                    _gridSelectGroup.UpdateSelections();
                    _gridSelectGroup.SelectRight();
                }
                else if (value.x < -0.1f)
                {
                    _gridSelectGroup.UpdateSelections();
                    _gridSelectGroup.SelectLeft();
                }
                else if (value.y > 0.1f)
                {
                    _gridSelectGroup.UpdateSelections();
                    _gridSelectGroup.SelectUp();
                }
                else if (value.y < -0.1f)
                {
                    _gridSelectGroup.UpdateSelections();
                    _gridSelectGroup.SelectDown();
                }
            }
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
