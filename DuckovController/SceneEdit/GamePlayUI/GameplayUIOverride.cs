using Duckov.MiniMaps.UI;
using Duckov.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayUIOverride : MonoBehaviour
    {
        private void Awake()
        {
            Patch();
            InitInputAction();
        }

        private void Patch()
        {
            var viewTabs = GetComponentInChildren<ViewTabs>();
            viewTabs.gameObject.AddComponent<ViewTabsOverride>();
            var playerStatusView = GetComponentInChildren<PlayerStatsView>();
            playerStatusView.gameObject.AddComponent<PlayerStatusViewOverride>();
            var miniMapView = GetComponentInChildren<MiniMapView>();
            miniMapView.gameObject.AddComponent<MiniMapViewOverride>();
        }

        private void OnEnable()
        {
            RegInput();
            View.OnActiveViewChanged += OnActiveViewChanged;
        }

        private void OnDisable()
        {
            UnRegInput();
            View.OnActiveViewChanged -= OnActiveViewChanged;
        }

        private void OnActiveViewChanged()
        {
            Debug.Log($"CurView == {View.ActiveView?.name}");
        }
    }
}
