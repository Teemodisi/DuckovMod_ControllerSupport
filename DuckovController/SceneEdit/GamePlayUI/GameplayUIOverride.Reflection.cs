using System.Reflection;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayUIOverride
    {
        private InputAction CancelAction => (InputAction)Reflection.CancelActionField.GetValue(UIInputManager.Instance);

        private static class Reflection
        {
            private static readonly BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;

            public static FieldInfo CancelActionField { get; } = typeof(UIInputManager)
                .GetField("inputActionCancel", binding);
        }
    }
}
