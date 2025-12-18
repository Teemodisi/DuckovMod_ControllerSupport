using Duckov.UI;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewSubPatchEquipment : LootViewSubSelection
    {
        private GridLayoutGroup _equipmentGridLayout;

        private GridLayoutGroup _inventoryGridLayout;

        private GridLayoutGroup _petLayout;

        private IGridSelectGroup _curSelectingPart;

        private GridSelectGroup<SlotDisplay> _equipmentSelectGroup;

        private GridSelectGroup<InventoryEntry> _inventorySelectGroup;

        private GridSelectGroup<InventoryEntry> _petSelectGroup;

        protected override string SelectorPatchSubPath => "Content";

        protected override void Awake()
        {
            _equipmentGridLayout = transform.FindWithDebug("Content/ItemSlotsDisplay/GridLayout")
                .GetComponent<GridLayoutGroup>();
            _inventoryGridLayout = transform
                .FindWithDebug("Content/Scroll View/Viewport/Content/InventoryDisplay/Container/Layout")
                .GetComponent<GridLayoutGroup>();
            _petLayout = transform.FindWithDebug("Content/InventoryDisplay_Pet/Container/Layout")
                .GetComponent<GridLayoutGroup>();

            base.Awake();

            _equipmentSelectGroup = new GridSelectGroup<SlotDisplay>(
                _equipmentGridLayout,
                () => _equipmentGridLayout.GetComponentsInChildren<SlotDisplay>(),
                OnInventoryEntrySelect,
                OnInventoryEntryDeselect);
            _inventorySelectGroup = new GridSelectGroup<InventoryEntry>(
                _inventoryGridLayout,
                () => _inventoryGridLayout.GetComponentsInChildren<InventoryEntry>(),
                OnInventoryEntrySelect,
                OnInventoryEntryDeselect);
            _petSelectGroup = new GridSelectGroup<InventoryEntry>(
                _petLayout,
                () => _petLayout.GetComponentsInChildren<InventoryEntry>(),
                OnInventoryEntrySelect,
                OnInventoryEntryDeselect);

            _equipmentSelectGroup.onRightEdge += SelectEqu2Pet;
            _equipmentSelectGroup.onDownEdge += SelectEqu2Inv;
            _inventorySelectGroup.onRightEdge += SelectInv2Pet;
            _inventorySelectGroup.onUpEdge += SelectInv2Equ;
            _petSelectGroup.onLeftEdge += SelectPet2Equ;
        }

        private static void OnInventoryEntrySelect(SlotDisplay entry, int index)
        {
            entry.gameObject.EmitEventPointerEnter();
            var itemDisplay = (ItemDisplay)Reflection.slotDisplayGetItemDisplay.GetValue(entry);
            ItemUIUtilities.Select(itemDisplay);
        }

        private static void OnInventoryEntrySelect(InventoryEntry entry, int index)
        {
            entry.gameObject.EmitEventPointerEnter();
            var itemDisplay = (ItemDisplay)Reflection.inventoryDisplayGetItemDisplay.GetValue(entry);
            ItemUIUtilities.Select(itemDisplay);
        }

        private static void OnInventoryEntryDeselect(SlotDisplay entry, int index)
        {
            entry.gameObject.EmitEventPointerExit();
        }

        private static void OnInventoryEntryDeselect(InventoryEntry entry, int index)
        {
            entry.gameObject.EmitEventPointerExit();
        }

        private void SelectEqu2Pet(Vector2Int fromPos)
        {
            _curSelectingPart = _petSelectGroup;
            var index = Mathf.Clamp(fromPos.y, 0, _curSelectingPart.GroupLength - 1);
            _curSelectingPart.Select(index);
        }

        private void SelectEqu2Inv(Vector2Int fromPos)
        {
            _curSelectingPart = _inventorySelectGroup;
            var index = Mathf.Clamp(fromPos.x, 0, _curSelectingPart.XCount - 1);
            _curSelectingPart.Select(index);
        }

        private void SelectInv2Pet(Vector2Int fromPos)
        {
            _curSelectingPart = _petSelectGroup;
            var index = Mathf.Clamp(fromPos.y - 2, 0, _curSelectingPart.GroupLength - 1);
            _curSelectingPart.Select(index);
        }

        private void SelectInv2Equ(Vector2Int fromPos)
        {
            _curSelectingPart = _equipmentSelectGroup;
            var x = Mathf.Clamp(fromPos.x, 0, _curSelectingPart.XCount - 1);
            var y = Mathf.Max(0, _curSelectingPart.YCount - 1);
            _curSelectingPart.Select(x + y * _curSelectingPart.XCount);
        }

        private void SelectPet2Equ(Vector2Int fromPos)
        {
            if (fromPos.y < 2)
            {
                _curSelectingPart = _equipmentSelectGroup;
                var x = _curSelectingPart.XCount - 1;
                var y = Mathf.Clamp(fromPos.y, 0, _curSelectingPart.YCount - 1);
                _curSelectingPart.Select(x + y * _curSelectingPart.XCount);
            }
            else
            {
                _curSelectingPart = _inventorySelectGroup;
                var x = _curSelectingPart.XCount - 1;
                var y = Mathf.Clamp(fromPos.y - 2, 0, _curSelectingPart.YCount - 1);
                _curSelectingPart.Select(x + y * _curSelectingPart.XCount);
            }
        }

        private void OnNavigationInput(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            if (_curSelectingPart == null)
            {
                _curSelectingPart = _equipmentSelectGroup;
            }
            var value = context.ReadValue<Vector2>();
            if (value.x > 0.1f)
            {
                _curSelectingPart.UpdateSelections();
                _curSelectingPart.SelectRight();
            }
            else if (value.x < -0.1f)
            {
                _curSelectingPart.UpdateSelections();
                _curSelectingPart.SelectLeft();
            }
            else if (value.y > 0.1f)
            {
                _curSelectingPart.UpdateSelections();
                _curSelectingPart.SelectUp();
            }
            else if (value.y < -0.1f)
            {
                _curSelectingPart.UpdateSelections();
                _curSelectingPart.SelectDown();
            }
        }
    }
}
