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

            var navigate = inputActionMap.AddAction("Navigate", expectedControlLayout: "Axis");
            navigate.AddCompositeBinding("1DAxis")
                .With("positive", InputSystemUtils.BindingDpadLeft)
                .With("negative", InputSystemUtils.BindingDpadRight);

            sort.BindInput(OnSortInput);
            navigate.BindInput(OnNavigateInput);
            moveLeftScroll.BindInput(OnMoveLeftScrollInput);
            moveRightScroll.BindInput(OnMoveRightScrollInput);
            
        }
    }
}
