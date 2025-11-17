using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class QuestViewOverride
    {
        protected override void InitInput(InputActionMap inputActionMap)
        {
            var sort = inputActionMap.AddAction("Sort", InputActionType.Button);
            sort.AddBinding("<GamePad>/buttonNorth");

            var moveLeftScroll = inputActionMap.AddAction("MoveLeftScroll");
            moveLeftScroll.AddBinding("<GamePad>/leftStick");
            
            var moveRightScroll = inputActionMap.AddAction("MoveRightScroll");
            moveRightScroll.AddBinding("<GamePad>/rightStick");

            var navigate = inputActionMap.AddAction("Navigate", expectedControlLayout: "Axis");
            navigate.AddCompositeBinding("1DAxis")
                .With("positive", "<Gamepad>/dpad/left")
                .With("negative", "<Gamepad>/dpad/right");

            sort.BindInput(OnSortInput);
            navigate.BindInput(OnNavigateInput);
            moveLeftScroll.BindInput(OnMoveLeftScrollInput);
            moveRightScroll.BindInput(OnMoveRightScrollInput);
            
        }
    }
}
