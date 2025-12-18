using System.Reflection;
using Duckov.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewSubPatchEquipment
    {
        private static class Reflection
        {
            private const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance ;
            
            public static readonly FieldInfo slotDisplayGetItemDisplay = typeof(SlotDisplay)
                .GetField("itemDisplay",flags);
            
            public static readonly FieldInfo inventoryDisplayGetItemDisplay = typeof(InventoryEntry)
                .GetField("itemDisplay",flags);
        }
        
    }
}
