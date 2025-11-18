using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class QuestViewOverride
    {
        protected override void InitInput(InputActionMap inputActionMap)
        {
            var sort = inputActionMap.AddAction("Sort", InputActionType.Button);
            sort.AddBinding(InputSystemUtils.BindingYButton);

            var moveLeftScroll = inputActionMap.AddAction("MoveLeftScroll");
            moveLeftScroll.AddBinding(InputSystemUtils.BindingLeftStick);

            var moveRightScroll = inputActionMap.AddAction("MoveRightScroll");
            moveRightScroll.AddBinding(InputSystemUtils.BindingRightStick);

            var switchTab = inputActionMap.AddAction("SwitchTab", expectedControlLayout: "Axis");
            switchTab.AddCompositeBinding("1DAxis")
                .With("positive", InputSystemUtils.BindingDpadRight)
                .With("negative", InputSystemUtils.BindingDpadLeft);

            var navigate = inputActionMap.AddAction("Navigate", expectedControlLayout: "Axis", interactions: "Repeat");
            navigate.AddCompositeBinding("1DAxis")
                .With("positive", InputSystemUtils.BindingDpadDown)
                .With("negative", InputSystemUtils.BindingDpadUp);

            sort.BindInput(OnSortInput);
            switchTab.BindInput(OnSwitchTabInput);
            navigate.BindInput(OnNavigateInput);
            moveLeftScroll.BindInput(OnMoveLeftScrollInput);
            moveRightScroll.BindInput(OnMoveRightScrollInput);
        }
    }
}
