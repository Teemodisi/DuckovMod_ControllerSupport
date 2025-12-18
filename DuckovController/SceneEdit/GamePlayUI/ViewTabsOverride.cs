using Duckov.MasterKeys.UI;
using Duckov.MiniMaps.UI;
using Duckov.Quests.UI;
using Duckov.UI;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride : AbstractPatch
    {
        private RectTransform _horizontalRect;

        private SelectionGroup<GenericButton> _selectionGroup;

        protected override void Awake()
        {
            _horizontalRect = transform.Find("ViewButtons").gameObject.GetComponent<RectTransform>();
            base.Awake();
            _selectionGroup = new SelectionGroup<GenericButton>(
                () => _horizontalRect.gameObject.GetComponentsInChildren<GenericButton>(),
                (button, index) => { button.onPointerClick.Invoke(); },
                selectorIndex: selection =>
                {
                    var curView = View.ActiveView;
                    if (curView == null || curView == LootView.Instance)
                    {
                        return 0;
                    }
                    if (curView == PlayerStatsView.Instance)
                    {
                        return 1;
                    }
                    if (curView == QuestView.Instance)
                    {
                        return 2;
                    }
                    if (curView == MiniMapView.Instance)
                    {
                        return 3;
                    }
                    if (curView == MasterKeysView.Instance)
                    {
                        return 4;
                    }
                    if (curView.GetType() == typeof(NoteIndexView))
                    {
                        return 5;
                    }
                    return 0;
                }
            );
        }

        private void OnLeftNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _selectionGroup.SelectPrev();
            }
        }

        private void OnRightNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _selectionGroup.SelectNext();
            }
        }
    }
}
