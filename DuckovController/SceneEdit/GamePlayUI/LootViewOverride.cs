using Duckov.UI;
using DuckovController.Helper;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public class LootViewOverride : AbstractPatch
    {
        protected override void Awake()
        {
            base.Awake();

            var shortCount = GetComponentInChildren<ItemShortcutEditorPanel>();
            shortCount.gameObject.AddComponent<ItemShortcutEditorPanelOverride>();
        }
    }
}
