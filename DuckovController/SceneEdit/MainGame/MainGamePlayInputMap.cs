using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public class MainGamePlayInputMap
    {
        public MainGamePlayInputMap()
        {
            Map = new InputActionMap(nameof(MainGamePlayInputMap));

            RunAction = Map.AddAction("Run", InputActionType.Button);
            RunAction.AddBinding("<Gamepad>/buttonEast");
            RunAction.AddBinding("<Gamepad>/leftStickPress");

            CancelAction = Map.AddAction("Cancel", InputActionType.Button);
            CancelAction.AddBinding("<Gamepad>/buttonEast");

            RollAction = Map.AddAction("Roll", InputActionType.Button);
            RollAction.AddBinding("<Gamepad>/buttonSouth");

            InteractAction = Map.AddAction("Interact", InputActionType.Button);
            InteractAction.AddBinding("<Gamepad>/buttonNorth");

            ReloadAction = Map.AddAction("Reload", InputActionType.Button);
            ReloadAction.AddBinding("<Gamepad>/buttonWest");

            MovementAction = Map.AddAction("Movement", expectedControlLayout: "Vector2");
            MovementAction.AddBinding("<Gamepad>/leftStick").WithProcessors("StickDeadzone");

            AimDirectionAction = Map.AddAction("AimDirection", expectedControlLayout: "Vector2");
            AimDirectionAction.AddBinding("<Gamepad>/rightStick").WithProcessors("StickDeadzone");

            AdsAction = Map.AddAction("Aim");
            AdsAction.AddBinding("<Gamepad>/leftTrigger");

            TriggerAction = Map.AddAction("Trigger");
            TriggerAction.AddBinding("<Gamepad>/rightTrigger");

            // 另外的做法 详见 MainGameInputOverride
            // SwitchBullet = Map.AddAction("SwitchBullet", InputActionType.Button);
            // SwitchBullet.AddBinding("<Gamepad>/dpad/right");

            SwitchMeleeOrHold2ToPutAwayWeapon = Map.AddAction("SwitchMeleeOrPutAway", InputActionType.Button);
            SwitchMeleeOrHold2ToPutAwayWeapon.AddBinding("<Gamepad>/dpad/left").WithInteractions("Hold,Press");

            SwitchWeapon = Map.AddAction("SwitchWeapon", InputActionType.Button);
            SwitchWeapon.AddBinding("<Gamepad>/leftShoulder");

            UseItemOrHold2OpenTurntable = Map.AddAction("UseItemOrOpenTurntable", InputActionType.Button);
            UseItemOrHold2OpenTurntable.AddBinding("<Gamepad>/rightShoulder").WithInteractions("Hold(duration=0.2),Press");

            SmallMenuNavigateUp = Map.AddAction("SmallMenuNavigationUp", InputActionType.Button);
            SmallMenuNavigateUp.AddBinding("<Gamepad>/dpad/up");

            SmallMenuNavigateDown = Map.AddAction("SmallMenuNavigationDown", InputActionType.Button);
            SmallMenuNavigateDown.AddBinding("<Gamepad>/dpad/down");

            OpenInventory = Map.AddAction("OpenInventory", InputActionType.Button);
            OpenInventory.AddBinding("<Gamepad>/select");

            OpenMenu = Map.AddAction("OpenMenu", InputActionType.Button);
            OpenMenu.AddBinding("<Gamepad>/start");

            QuackAction = Map.AddAction("Quack", InputActionType.Button);
            QuackAction.AddBinding("<Gamepad>/rightStickPress");
        }

        public InputActionMap Map { get; }

        public InputAction RunAction { get; }

        public InputAction CancelAction { get; }

        public InputAction RollAction { get; }

        public InputAction InteractAction { get; }

        public InputAction ReloadAction { get; }

        public InputAction MovementAction { get; }

        public InputAction AimDirectionAction { get; }

        public InputAction AdsAction { get; }

        public InputAction TriggerAction { get; }

        public InputAction SmallMenuNavigateUp { get; }

        public InputAction SmallMenuNavigateDown { get; }

        // 另外的做法 详见 MainGameInputOverride
        // public InputAction SwitchBullet { get; }

        public InputAction SwitchMeleeOrHold2ToPutAwayWeapon { get; }

        public InputAction SwitchWeapon { get; }

        public InputAction UseItemOrHold2OpenTurntable { get; }

        public InputAction OpenMenu { get; }

        public InputAction OpenInventory { get; }

        public InputAction QuackAction { get; }
    }
}
