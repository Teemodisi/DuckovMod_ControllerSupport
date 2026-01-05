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
            _moveMapAction.AddBinding(InputSystemUtils.BindingLeftStick);

            _moveMouseAction = inputActionMap.AddAction("MoveMouse");
            _moveMouseAction.AddBinding(InputSystemUtils.BindingRightStick);

            _zoomAction = inputActionMap.AddAction("ZoomUp", expectedControlLayout: "Axis");
            _zoomAction.AddCompositeBinding("1DAxis")
                .With("positive", InputSystemUtils.BindingRightTrigger)
                .With("negative", InputSystemUtils.BindingLeftTrigger);

            _pinAction = inputActionMap.AddAction("Pin", InputActionType.Button);
            _pinAction.AddBinding(InputSystemUtils.BindingAButton);

            _centerPlayerAction = inputActionMap.AddAction("CenterPlayer", InputActionType.Button);
            _centerPlayerAction.AddBinding(InputSystemUtils.BindingLeftStickPress);

            _selectColorAndIconAction = inputActionMap
                .AddAction("SelectColorAndIcon", expectedControlLayout: "Vector2");
            _selectColorAndIconAction.AddCompositeBinding("2DVector(mode=1)")
                .With("up", InputSystemUtils.BindingDpadUp)
                .With("down", InputSystemUtils.BindingDpadDown)
                .With("left", InputSystemUtils.BindingDpadLeft)
                .With("right", InputSystemUtils.BindingDpadRight);

            _moveMapAction.BindInput(OnMoveInput);
            _moveMouseAction.BindInput(OnMoveMouseInput);
            _zoomAction.BindInput(OnZoomInput);
            _pinAction.BindInput(OnPinInput);
            _centerPlayerAction.BindInput(OnCenterPlayerInput);
            _selectColorAndIconAction.BindInput(OnSelectColorAndIconInput);
        }
    }
}
