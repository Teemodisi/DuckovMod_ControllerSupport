using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride
    {
        private void Patch()
        {
            var buttonsRect = transform.Find("ViewButtons").gameObject.GetComponent<RectTransform>();
            var lbButton = UIStyle.DrawPadButtonIcon(buttonsRect, UIStyle.GamePadButton.LB);
            lbButton.pivot = new Vector2(1, 0.5f);
            lbButton.anchorMin = new Vector2(0, 0.5f);
            lbButton.anchorMax = new Vector2(0, 0.5f);
            lbButton.anchoredPosition = new Vector2(-50, 0);
            lbButton.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            lbButton.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
            var rbButton = UIStyle.DrawPadButtonIcon(buttonsRect, UIStyle.GamePadButton.RB);
            rbButton.pivot = new Vector2(0, 0.5f);
            rbButton.anchorMin = new Vector2(1, 0.5f);
            rbButton.anchorMax = new Vector2(1, 0.5f);
            rbButton.anchoredPosition = new Vector2(50, 0);
            rbButton.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
            rbButton.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
