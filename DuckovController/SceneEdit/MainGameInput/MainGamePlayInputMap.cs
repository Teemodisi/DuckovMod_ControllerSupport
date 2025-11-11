using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGameInput
{
    public class MainGamePlayInputMap
    {
        public MainGamePlayInputMap()
        {
            Map = new InputActionMap();

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

            MovementAction = Map.AddAction("Movement");
            MovementAction.AddBinding("<Gamepad>/leftStick");

            AimDirectionAction = Map.AddAction("AimDirection");
            AimDirectionAction.AddBinding("<Gamepad>/rightStick");

            AdsAction = Map.AddAction("Aim");
            AdsAction.AddBinding("<Gamepad>/leftTrigger");

            TriggerAction = Map.AddAction("Trigger");
            TriggerAction.AddBinding("<Gamepad>/rightTrigger");

            // 另外的做法 详见 MainGameInputOverride
            // SwitchBullet = Map.AddAction("SwitchBullet", InputActionType.Button);
            // SwitchBullet.AddBinding("<Gamepad>/dpad/right");

            SwitchMeleeWeapon = Map.AddAction("SwitchMeleeWeapon", InputActionType.Button);
            SwitchMeleeWeapon.AddBinding("<Gamepad>/dpad/left");

            PutAwayWeapon = Map.AddAction("PutAwayWeapon", InputActionType.Button);
            // PutAwayWeapon.AddBinding("<Gamepad>/dpad/left");

            SwitchWeapon = Map.AddAction("SwitchWeapon", InputActionType.Button);
            SwitchWeapon.AddBinding("<Gamepad>/leftShoulder");

            UseItem = Map.AddAction("UseItem", InputActionType.Button);
            UseItem.AddBinding("<Gamepad>/rightShoulder");

            OpenItemTurntable = Map.AddAction("OpenItemTurntable", InputActionType.Button);
            // OpenItemTurntable.AddBinding("<Gamepad>/rightShoulder");

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

        public InputAction SwitchMeleeWeapon { get; }

        public InputAction PutAwayWeapon { get; }

        public InputAction SwitchWeapon { get; }

        public InputAction UseItem { get; }

        public InputAction OpenItemTurntable { get; }

        public InputAction OpenMenu { get; }

        public InputAction OpenInventory { get; }

        public InputAction QuackAction { get; }
    }
}
