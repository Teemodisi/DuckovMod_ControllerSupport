using UnityEngine;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class QuestViewOverride
    {
        protected override void Patch()
        {
            var activeQuestBtnRect = _activeQuestButton.GetComponent<RectTransform>();
            activeQuestBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            var leftIcon = UIStyle.DrawPadButtonIcon(activeQuestBtnRect, UIStyle.GamePadButton.Left);
            leftIcon.pivot = new Vector2(1f, 0.5f);
            leftIcon.anchorMin = new Vector2(1f, 0.5f);
            leftIcon.anchorMax = new Vector2(1f, 0.5f);
            leftIcon.anchoredPosition = new Vector2(-50f, 0f);
            leftIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            var historyQuestBtnRect = _historyQuestButton.GetComponent<RectTransform>();
            historyQuestBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            var rightIcon = UIStyle.DrawPadButtonIcon(historyQuestBtnRect, UIStyle.GamePadButton.Right);
            rightIcon.pivot = new Vector2(1f, 0.5f);
            rightIcon.anchorMin = new Vector2(1f, 0.5f);
            rightIcon.anchorMax = new Vector2(1f, 0.5f);
            rightIcon.anchoredPosition = new Vector2(-50f, 0f);
            rightIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            var sortBtnRect = _sortingButton.GetComponent<RectTransform>();
            sortBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            var sortIcon = UIStyle.DrawPadButtonIcon(sortBtnRect, UIStyle.GamePadButton.Y);
            sortIcon.pivot = new Vector2(1f, 0.5f);
            sortIcon.anchorMin = new Vector2(1f, 0.5f);
            sortIcon.anchorMax = new Vector2(1f, 0.5f);
            sortIcon.anchoredPosition = new Vector2(-50f, 0f);
            sortIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            //Tips
            var tips = UIStyle.GamePadTipsRectTransform(transform.GetComponent<RectTransform>());
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.PickSelection,
                new[] { UIStyle.GamePadButton.Up, UIStyle.GamePadButton.Down });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.SlideLeft,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.Exit,
                new[] { UIStyle.GamePadButton.Menu });
        }
    }
}
