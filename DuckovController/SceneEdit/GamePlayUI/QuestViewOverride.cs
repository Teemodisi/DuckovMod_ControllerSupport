using Cysharp.Threading.Tasks;
using Duckov.Quests.UI;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
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

        private SelectionGroup<QuestEntry> _selectionGroup;

        private ScrollRect _leftScrollRect;

        protected override void Awake()
        {
            var tabs = transform.FindWithDebug("Content/Selection/Tabs");
            _activeQuestButton = tabs.FindWithDebug("Btn_Active").GetComponent<Button>();
            _historyQuestButton = tabs.FindWithDebug("Btn_History").GetComponent<Button>();
            _sortingButton = transform.FindWithDebug("Content/Selection/SortingBar/Btn_Sort")
                .GetComponent<QuestSortButton>();
            _leftScrollViewGamepadControl = transform.FindWithDebug("Content/Selection/Scroll View")
                .gameObject.AddComponent<ScrollViewGamepadControl>();
            _leftScrollRect = _leftScrollViewGamepadControl.GetComponent<ScrollRect>();
            _rightScrollViewGamepadControl = transform.FindWithDebug("Content/Details/Content/Scroll View")
                .gameObject.AddComponent<ScrollViewGamepadControl>();
            base.Awake();
            _selectionGroup = new SelectionGroup<QuestEntry>(
                () => _leftScrollRect.content.GetComponentsInChildren<QuestEntry>(),
                (entry, i) => { entry.gameObject.EmitEventPointerClickAndDownBtnLeft(); },
                null,
                list =>
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        if (list[i].Selected)
                        {
                            return i;
                        }
                    }
                    return -1;
                },
                false
            );
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _selectionGroup.UpdateSelections();
            FocusSelection();
        }

        private void OnSwitchTabInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var value = context.ReadValue<float>();
                if (value > 0.1f)
                {
                    //不知道为什么这里两个按钮用事件触发不到button的Invoke
                    _historyQuestButton.gameObject.EmitEventPointerClickAndDownBtnLeft();
                    _historyQuestButton.onClick.Invoke();
                    _selectionGroup.UpdateSelections();
                    FocusSelectionDelay().Forget();
                }
                else if (value < -0.1f)
                {
                    _activeQuestButton.gameObject.EmitEventPointerClickAndDownBtnLeft();
                    _activeQuestButton.onClick.Invoke();
                    _selectionGroup.UpdateSelections();
                    FocusSelectionDelay().Forget();
                }
            }
        }

        private void OnNavigateInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //面对这种按钮重建逻辑只能这样了捏
                _selectionGroup.UpdateSelections();
                var value = context.ReadValue<float>();
                if (value > 0.1f)
                {
                    _selectionGroup.SelectNext();
                    FocusSelection();
                }
                else if (value < -0.1f)
                {
                    _selectionGroup.SelectPrev();
                    FocusSelection();
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
                if (_selectionGroup.CurrentSelection != null && _selectionGroup.Selections != null)
                {
                    var curID = _selectionGroup.CurrentSelection.Target.ID;
                    _sortingButton.gameObject.EmitEventPointerClickAndDownBtnLeft();
                    for (var i = 0; i < _selectionGroup.Selections.Count; i++)
                    {
                        if (_selectionGroup.Selections[i].Target.ID == curID)
                        {
                            _selectionGroup.Selections[i].gameObject.EmitEventPointerClickAndDownBtnLeft();
                            break;
                        }
                    }
                    FocusSelectionDelay().Forget();
                }
                else
                {
                    _sortingButton.gameObject.EmitEventPointerClickAndDownBtnLeft();
                }
            }
        }

        private void FocusSelection()
        {
            _leftScrollViewGamepadControl.TryToFocusContentObject(
                _selectionGroup.CurrentSelection?.GetComponent<RectTransform>());
        }

        private async UniTask FocusSelectionDelay()
        {
            // 等待一帧，确保滚动视图更新
            await UniTask.DelayFrame(1);
            FocusSelection();
        }
    }
}
