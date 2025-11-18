using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class NoteIndexViewOverride
    {
        protected override void InitInput(InputActionMap inputActionMap)
        {
            var navigate = inputActionMap.AddAction("Navigate",
                expectedControlLayout: "Axis", interactions: "Repeat");
            navigate.AddCompositeBinding("1DAxis")
                .With("positive", InputSystemUtils.BindingDpadDown)
                .With("negative", InputSystemUtils.BindingDpadUp);

            var leftScroll = inputActionMap.AddAction("LeftScrollView");
            leftScroll.AddBinding(InputSystemUtils.BindingLeftStick);

            var rightScroll = inputActionMap.AddAction("RightScrollView");
            rightScroll.AddBinding(InputSystemUtils.BindingRightStick);

            navigate.BindInput(OnNavigationInput);
            leftScroll.BindInput(OnLeftScrollInput);
            rightScroll.BindInput(OnRightScrollInput);
        }
    }
}
