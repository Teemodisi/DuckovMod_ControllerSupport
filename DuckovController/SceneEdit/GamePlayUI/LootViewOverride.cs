using Duckov;
using Duckov.UI;
using UnityEngine;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewOverride : AbstractPatch
    {
        private LootView _lootView;

        private ItemShortcutEditorPanel _shortcutEditorPanel;

        private ItemSlotCollectionDisplay _characterSlotCollectionDisplay;

        private InventoryDisplay _characterInventoryDisplay;

        private InventoryDisplay _petInventoryDisplay;

        private InventoryDisplay _lootTargetInventoryDisplay;

        private InventoryFilterDisplay _inventoryFilterDisplay;

        private ItemDetailsDisplay _detailsDisplay;

        private LootViewSubSelection _leftRect;

        private LootViewSubSelection _topRect;

        private LootViewSubSelection _rightRect;

        private LootViewSubSelection _bottomRect;

        private LootViewSubSelection _currentSelectionArea;

        protected override void Awake()
        {
            _lootView = GetComponent<LootView>();
            _characterSlotCollectionDisplay = (ItemSlotCollectionDisplay)Reflection
                .CharacterSlotCollectionDisplayFieldInfo
                .GetValue(_lootView);
            _characterInventoryDisplay = (InventoryDisplay)Reflection.CharacterInventoryDisplayFieldInfo
                .GetValue(_lootView);
            _petInventoryDisplay = (InventoryDisplay)Reflection.PetInventoryDisplayFieldInfo
                .GetValue(_lootView);
            _lootTargetInventoryDisplay = (InventoryDisplay)Reflection.LootTargetInventoryDisplayFieldInfo
                .GetValue(_lootView);
            _inventoryFilterDisplay = (InventoryFilterDisplay)Reflection.LootTargetFilterDisplayFieldInfo
                .GetValue(_lootView);
            _detailsDisplay = (ItemDetailsDisplay)Reflection.DetailsDisplayFieldInfo
                .GetValue(_lootView);

            _leftRect = transform.Find("Main/EquipmentAndInventory")
                .gameObject.AddComponent<LootViewSubPatchEquipment>();
            _rightRect = transform.Find("Main/LootTarget")
                .gameObject.AddComponent<LootViewSubPatchTarget>();
            _topRect = transform.Find("ItemDetails/Panel")
                .gameObject.AddComponent<LootViewSubPatchDetails>();
            _bottomRect = transform.Find("Main/ShortcutEditor")
                .gameObject.AddComponent<LootViewSubPatchShortcut>();

            base.Awake();
        }

        private void Update()
        {
            if (!_leftTriggerPressed)
            {
                return;
            }
            if (_currentSelectionArea == null)
            {
                SelectSelection(_leftRect);
            }
            if (_selectDirection.sqrMagnitude < 0.01f)
            {
                return;
            }
            if (TrySelect(_leftRect, Vector2.left)) { }
            else if (TrySelect(_topRect, Vector2.up)) { }
            else if (TrySelect(_rightRect, Vector2.right)) { }
            else if (TrySelect(_bottomRect, Vector2.down)) { }
            return; 

            bool TrySelect(LootViewSubSelection selection, Vector2 dir)
            {
                const float threshold = 0.75f;
                if (_currentSelectionArea != selection && Vector2.Dot(_selectDirection, dir) > threshold)
                {
                    if (selection.gameObject.activeInHierarchy)
                    {
                        SelectSelection(selection);
                    }
                    return true;
                }
                return false;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_currentSelectionArea == null || !_currentSelectionArea.gameObject.activeInHierarchy)
            {
                SelectSelection(_leftRect);
            }
            else
            {
                _currentSelectionArea?.OnSelect();
            }
        }

        private void SelectSelection(LootViewSubSelection selection)
        {
            _currentSelectionArea?.OnDeselect();
            selection.OnSelect();
            _currentSelectionArea = selection;
            AudioManager.Post("UI/hover");
        }
    }
}
