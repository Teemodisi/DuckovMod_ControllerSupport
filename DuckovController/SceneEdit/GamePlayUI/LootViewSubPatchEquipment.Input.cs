using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewSubPatchEquipment
    {
        protected override void InitInput(InputActionMap inputActionMap)
        {
            var navigation = inputActionMap.AddAction("Navigation",
                expectedControlLayout: "Vector2", interactions: "Repeat");
            navigation.AddCompositeBinding("2DVector(mode=1)")
                .With("up", InputSystemUtils.BindingDpadUp)
                .With("down", InputSystemUtils.BindingDpadDown)
                .With("left", InputSystemUtils.BindingDpadLeft)
                .With("right", InputSystemUtils.BindingDpadRight);

            var scroll = inputActionMap.AddAction("Scroll");
            scroll.AddBinding(InputSystemUtils.BindingLeftStick);

            navigation.BindInput(OnNavigationInput);
            scroll.BindInput(OnScrollInput);
        }
    }
}
