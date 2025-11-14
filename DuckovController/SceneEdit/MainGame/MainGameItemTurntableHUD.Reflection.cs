using System.Reflection;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameItemTurntableHUD
    {
        private void RefShortCutInput(int index)
        {
            Reflection.cicShortInputParam[0] = index;
            Reflection.CicShortInput.Invoke(CharacterInputControl.Instance, Reflection.cicShortInputParam);
        }

        private static class Reflection
        {
            public static readonly object[] cicShortInputParam = new object[1];

            public static MethodInfo CicShortInput { get; } = typeof(CharacterInputControl)
                .GetMethod("ShortCutInput", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
