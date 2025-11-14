using System;
using System.Reflection;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameInputOverride
    {
        private InputAction SwitchBulletInputAction => ReflectionPart.SwitchBulletInputAction;

        private Vector2 CharInputCtrlMouseDelta
        {
            get => (Vector2)ReflectionPart.CicMouseDeltaFieldInfo.GetValue(CharacterInputControl.Instance);
            set => ReflectionPart.CicMouseDeltaFieldInfo.SetValue(CharacterInputControl.Instance, value);
        }

        // private void RefStoreHoldWeaponBeforeUseMethodInfo()
        // {
        //     ReflectionPart.StoreHoldWeaponBeforeUseMethodInfo.Invoke(CharacterMainControl.Main, null);
        // }

        private static class ReflectionPart
        {
            private static FieldInfo s_SwitchBulletTypeFieldInfo;

            private static InputAction s_SwitchBulletInputAction;

            public static FieldInfo CicMouseDeltaFieldInfo { get; } =
                ReflectionUtils.FindField<CharacterInputControl>("mouseDelta");

            private static FieldInfo CicInputActionsFieldInfo { get; } =
                ReflectionUtils.FindField<CharacterInputControl>("inputActions");

            //damn 这个交互怎么是这样捏
            public static InputAction SwitchBulletInputAction
            {
                get
                {
                    if (s_SwitchBulletInputAction == null)
                    {
                        object fieldObj = null;
                        if (s_SwitchBulletTypeFieldInfo == null)
                        {
                            fieldObj = CicInputActionsFieldInfo.GetValue(CharacterInputControl.Instance);
                            var priType = fieldObj.GetType();
                            s_SwitchBulletTypeFieldInfo = priType.GetField("SwitchBulletType",
                                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                            if (s_SwitchBulletTypeFieldInfo == null)
                            {
                                throw new Exception("找不到SwitchBulletInputAction");
                            }
                        }
                        if (fieldObj == null)
                        {
                            fieldObj = CicInputActionsFieldInfo.GetValue(CharacterInputControl.Instance);
                        }
                        s_SwitchBulletInputAction =
                            s_SwitchBulletTypeFieldInfo.GetValue(fieldObj) as InputAction;
                    }
                    return s_SwitchBulletInputAction;
                }
            }

            // public static MethodInfo StoreHoldWeaponBeforeUseMethodInfo { get; } =
            //     typeof(CharacterMainControl).GetMethod("StoreHoldWeaponBeforeUse",
            //         BindingFlags.Instance | BindingFlags.NonPublic);

            // private static FieldInfo GameCameraAimingTypeFieldInfo { get; } =
            //     ReflectionUtils.FindField<GameCamera>("cameraAimingType");
            //
            // private static GameCamera.CameraAimingTypes CameraAimingType =>
            //     (GameCamera.CameraAimingTypes)GameCameraAimingTypeFieldInfo.GetValue(GameCamera.Instance);
            //
            // //Typo bro
            // private static FieldInfo CharInputScrollYFieldInfo { get; } =
            //     ReflectionUtils.FindField<CharacterInputControl>("scollY");
            //
            // private static float CharInputScrollY
            // {
            //     get => (float)CharInputScrollYFieldInfo.GetValue(CharacterInputControl.Instance);
            //     set => CharInputScrollYFieldInfo.SetValue(CharacterInputControl.Instance, value);
            // }
        }
    }
}
