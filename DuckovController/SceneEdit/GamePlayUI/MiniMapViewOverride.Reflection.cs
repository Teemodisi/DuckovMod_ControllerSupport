using System.Reflection;
using Duckov.MiniMaps.UI;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MiniMapViewOverride
    {
        private float RefZoom
        {
            get => (float)Reflection.ZoomPropInfo.GetValue(MiniMapView.Instance);
            set => Reflection.ZoomPropInfo.SetValue(MiniMapView.Instance, value);
        }

        private static class Reflection
        {
            private static readonly BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;

            public static PropertyInfo ZoomPropInfo { get; } = typeof(MiniMapView)
                .GetProperty("Zoom", binding);
        }
    }
}
