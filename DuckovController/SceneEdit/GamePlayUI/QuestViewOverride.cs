using Duckov.Quests.UI;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class QuestViewOverride : AbstractPatch
    {
        private Button _activeQuestButton;

        private Button _historyQuestButton;

        private QuestSortButton _sortingButton;

        private ScrollViewGamepadControl _leftScrollViewGamepadControl;

        private ScrollViewGamepadControl _rightScrollViewGamepadControl;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnNavigateInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var value = context.ReadValue<float>();
                if (value < -0.1f)
                {
                    _historyQuestButton.onClick.Invoke();
                }
                else if (value > 0.1f)
                {
                    _activeQuestButton.onClick.Invoke();
                }
            }
        }

        private void OnMoveLeftScrollInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _leftScrollViewGamepadControl.Move(context.ReadValue<Vector2>());
            }
            if (context.canceled)
            {
                _leftScrollViewGamepadControl.Move(Vector2.zero);
            }
        }

        private void OnMoveRightScrollInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _rightScrollViewGamepadControl.Move(context.ReadValue<Vector2>());
            }
            if (context.canceled)
            {
                _rightScrollViewGamepadControl.Move(Vector2.zero);
            }
        }

        private void OnSortInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _sortingButton.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
}
