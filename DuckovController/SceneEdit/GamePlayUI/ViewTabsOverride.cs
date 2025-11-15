using Duckov.MasterKeys.UI;
using Duckov.MiniMaps.UI;
using Duckov.Quests.UI;
using Duckov.UI;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride : MonoBehaviour
    {
        private RectTransform _horizontalRect;

        private GenericButton[] _tabButtons;

        private void Awake()
        {
            _horizontalRect = transform.Find("ViewButtons").gameObject.GetComponent<RectTransform>();
            _tabButtons = _horizontalRect.gameObject.GetComponentsInChildren<GenericButton>();
            Patch();
            InitInput();
        }

        private void OnEnable()
        {
            _inputActionMap?.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap?.Disable();
        }

        //更可靠的Index
        private int GetCurrentSelect()
        {
            var curView = View.ActiveView;
            if (curView == LootView.Instance)
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
            Debug.LogError($"{Utils.ModName} {nameof(ViewTabsOverride)} GetSelector Err");
            return 0;
        }

        private void OnLeftNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            var idx = GetCurrentSelect() - 1;
            if (idx < 0)
            {
                idx += _tabButtons.Length;
            }
            _tabButtons[idx].onPointerClick.Invoke();
        }

        private void OnRightNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            var idx = GetCurrentSelect() + 1;
            if (idx >= _tabButtons.Length)
            {
                idx -= _tabButtons.Length;
            }
            _tabButtons[idx].onPointerClick.Invoke();
        }
    }
}
