using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MiniMapViewOverride
    {

        private InputAction _moveMapAction;

        private InputAction _moveMouseAction;

        private InputAction _zoomAction;

        private InputAction _pinAction;

        private InputAction _centerPlayerAction;

        private InputAction _selectColorAndIconAction;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _moveMapAction = inputActionMap.AddAction("MoveMap");
            _moveMapAction.AddBinding("<Gamepad>/leftStick");

            _moveMouseAction = inputActionMap.AddAction("MoveMouse");
            _moveMouseAction.AddBinding("<Gamepad>/rightStick");

            _zoomAction = inputActionMap.AddAction("ZoomUp", expectedControlLayout: "Axis");
            _zoomAction.AddCompositeBinding("1DAxis")
                .With("positive", "<Gamepad>/rightTrigger")
                .With("negative", "<Gamepad>/leftTrigger");

            _pinAction = inputActionMap.AddAction("Pin", InputActionType.Button);
            _pinAction.AddBinding("<Gamepad>/buttonSouth");

            _centerPlayerAction = inputActionMap.AddAction("CenterPlayer", InputActionType.Button);
            _centerPlayerAction.AddBinding("<Gamepad>/leftStickPress");

            _selectColorAndIconAction = inputActionMap
                .AddAction("SelectColorAndIcon", expectedControlLayout: "Vector2");
            _selectColorAndIconAction.AddCompositeBinding("2DVector(mode=1)")
                .With("up", "<Gamepad>/dpad/up")
                .With("down", "<Gamepad>/dpad/down")
                .With("left", "<Gamepad>/dpad/left")
                .With("right", "<Gamepad>/dpad/right");

            _moveMapAction.BindInput(OnMoveInput);
            _moveMouseAction.BindInput(OnMoveMouseInput);
            _zoomAction.BindInput(OnZoomInput);
            _pinAction.BindInput(OnPinInput);
            _centerPlayerAction.BindInput(OnCenterPlayerInput);
            _selectColorAndIconAction.BindInput(OnSelectColorAndIconInput);
        }
    }
}
