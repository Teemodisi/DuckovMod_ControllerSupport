using System.Reflection;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayViewOverride
    {
        private InputAction CancelAction => (InputAction)Reflection.CancelActionField.GetValue(UIInputManager.Instance);

        private static class Reflection
        {
            private static readonly BindingFlags s_Binding = BindingFlags.Instance | BindingFlags.NonPublic;

            public static FieldInfo CancelActionField { get; } = typeof(UIInputManager)
                .GetField("inputActionCancel", s_Binding);
        }
    }
}
