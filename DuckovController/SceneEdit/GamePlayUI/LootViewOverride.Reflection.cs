using System.Reflection;
using Duckov.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewOverride
    {
        private static class Reflection
        {
            private const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            public static FieldInfo CharacterSlotCollectionDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("characterSlotCollectionDisplay", flags);

            public static FieldInfo CharacterInventoryDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("characterInventoryDisplay", flags);

            public static FieldInfo PetInventoryDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("petInventoryDisplay", flags);

            public static FieldInfo LootTargetInventoryDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("lootTargetInventoryDisplay", flags);

            public static FieldInfo LootTargetFilterDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("lootTargetFilterDisplay", flags);

            public static FieldInfo DetailsDisplayFieldInfo { get; } = typeof(LootView)
                .GetField("detailsDisplay", flags);
        }
    }
}
