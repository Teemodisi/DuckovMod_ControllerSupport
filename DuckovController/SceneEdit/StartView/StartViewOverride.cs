using DuckovController.Helper;
using UnityEngine;

namespace DuckovController.SceneEdit.StartView
{
    public class StartViewOverride: MonoBehaviour
    {
        private void Awake()
        {
            Patch();
        }

        private void Patch()
        {
            UIStyle.DebugRect(GetComponent<RectTransform>(),  new Color(1,1,1,0.2f) );
        }
    }
}
