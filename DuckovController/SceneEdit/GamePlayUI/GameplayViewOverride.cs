using Duckov.MiniMaps.UI;
using Duckov.UI;
using UnityEngine;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayViewOverride : AbstractPatch
    {
        protected override void Awake()
        {
            base.Awake();
            var viewTabs = GetComponentInChildren<ViewTabs>();
            viewTabs.gameObject.AddComponent<ViewTabsOverride>();
            var playerStatusView = GetComponentInChildren<PlayerStatsView>();
            playerStatusView.gameObject.AddComponent<PlayerStatusViewOverride>();
            var miniMapView = GetComponentInChildren<MiniMapView>();
            miniMapView.gameObject.AddComponent<MiniMapViewOverride>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            View.OnActiveViewChanged += OnActiveViewChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            View.OnActiveViewChanged -= OnActiveViewChanged;
        }

        private void OnActiveViewChanged()
        {
            Debug.Log($"CurView == {View.ActiveView?.name}");
        }
    }
}
