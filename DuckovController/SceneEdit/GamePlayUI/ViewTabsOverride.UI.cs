using UnityEngine;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride
    {
        private void Patch()
        {
            var buttonsRect = transform.Find("ViewButtons").gameObject.GetComponent<RectTransform>();
            buttonsRect.DebugRect(new Color(1, 1, 1, 0.2f));
        }
    }
}
