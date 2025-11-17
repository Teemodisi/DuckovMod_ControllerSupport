using Duckov.MasterKeys.UI;
using Duckov.MiniMaps.UI;
using Duckov.Quests.UI;
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
            var lootView = GetComponentInChildren<LootView>();
            lootView.gameObject.AddComponent<LootViewOverride>();      
            var playerStatusView = GetComponentInChildren<PlayerStatsView>();
            playerStatusView.gameObject.AddComponent<PlayerStatusViewOverride>();
            var questView = GetComponentInChildren<QuestView>();
            questView.gameObject.AddComponent<QuestViewOverride>();
            var miniMapView = GetComponentInChildren<MiniMapView>();
            miniMapView.gameObject.AddComponent<MiniMapViewOverride>();
            var masterKeysView = GetComponentInChildren<MasterKeysView>();
            masterKeysView.gameObject.AddComponent<MasterKeysViewOverride>();
            var noteView = GetComponentInChildren<NoteIndexView>();
            noteView.gameObject.AddComponent<NoteIndexViewOverride>();
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
