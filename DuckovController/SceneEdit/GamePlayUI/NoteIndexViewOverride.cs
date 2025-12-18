using Cysharp.Threading.Tasks;
using Duckov.NoteIndexs;
using Duckov.UI;
using Duckov.UI.Animations;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class NoteIndexViewOverride : AbstractPatch
    {
        private FadeGroup _fadeGroup;

        private ScrollRect _leftScrollView;

        private ScrollViewGamepadControl _leftScrollControl;

        private ScrollViewGamepadControl _rightScrollControl;

        private SelectionGroup<NoteIndexView_Entry> _selectionGroup;

        protected override void Awake()
        {
            _fadeGroup = GetComponent<FadeGroup>();
            _leftScrollView = transform.FindWithDebug("Content/LeftLayout/Entries Scroll View")
                .GetComponent<ScrollRect>();
            _leftScrollControl = _leftScrollView.gameObject.AddComponent<ScrollViewGamepadControl>();
            _rightScrollControl = transform.FindWithDebug("Content/Inspector/Scroll View")
                .GetComponent<ScrollRect>().gameObject.AddComponent<ScrollViewGamepadControl>();
            //不清楚原因，这里如果注册了UpdateSelection获取函数，这个时机获取的列表100%报错，很诡异的报空
            _selectionGroup = new SelectionGroup<NoteIndexView_Entry>(
                null,
                (entry, i) => entry.gameObject.EmitEventPointerClickAndDownBtnLeft(),
                loop: false
            );
            base.Awake();
            _fadeGroup.OnShowComplete += OnShowComplete;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _fadeGroup.OnShowComplete -= OnShowComplete;
        }

        private void OnShowComplete(FadeGroup obj)
        {
            _selectionGroup.UpdateSelections(UpdateSelectionFunction());
            if (_selectionGroup.GroupLength > 0 && _selectionGroup.CurrentSelection == null)
            {
                _selectionGroup.Select(0);
            }
        }

        private NoteIndexView_Entry[] UpdateSelectionFunction()
        {
            var unlock = NoteIndex.Instance.UnlockedNotes;
            var entries = _leftScrollView.content.GetComponentsInChildren<NoteIndexView_Entry>();
            var list = ListPool<NoteIndexView_Entry>.Get();
            foreach (var noteIndexViewEntry in entries)
            {
                if (noteIndexViewEntry != null && unlock.Contains(noteIndexViewEntry.key))
                {
                    list.Add(noteIndexViewEntry);
                }
            }
            var ans = list.ToArray();
            ListPool<NoteIndexView_Entry>.Release(list);
            return ans;
        }

        private void OnNavigationInput(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                var value = obj.ReadValue<float>();
                if (value > 0.1f)
                {
                    _selectionGroup.SelectNext();
                    _selectionGroup.UpdateSelections(UpdateSelectionFunction());
                    FocusSelectionDelay().Forget();
                }
                else if (value < -0.1f)
                {
                    _selectionGroup.SelectPrev();
                    _selectionGroup.UpdateSelections(UpdateSelectionFunction());
                    FocusSelectionDelay().Forget();
                }
            }
        }

        private void OnLeftScrollInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _leftScrollControl.Move(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                _leftScrollControl.Move(Vector2.zero);
            }
        }

        private void OnRightScrollInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _rightScrollControl.Move(context.ReadValue<Vector2>());
            }
            else if (context.canceled)
            {
                _rightScrollControl.Move(Vector2.zero);
            }
        }

        private void FocusSelection()
        {
            _leftScrollControl.TryToFocusContentObject(
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
