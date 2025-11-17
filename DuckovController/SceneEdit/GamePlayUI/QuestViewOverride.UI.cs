using Duckov.Quests.UI;
using DuckovController.Helper;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class QuestViewOverride
    {
        protected override void Patch()
        {
            var tabs = transform.FindWithDebug("Content/Selection/Tabs");

            var activeQuestBtnRect = tabs.FindWithDebug("Btn_Active").GetComponent<RectTransform>();
            activeQuestBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            _activeQuestButton = activeQuestBtnRect.GetComponent<Button>();
            var leftIcon = UIStyle.DrawPadButtonIcon(activeQuestBtnRect, UIStyle.GamePadButton.Left);
            leftIcon.pivot = new Vector2(1f, 0.5f);
            leftIcon.anchorMin = new Vector2(1f, 0.5f);
            leftIcon.anchorMax = new Vector2(1f, 0.5f);
            leftIcon.anchoredPosition = new Vector2(-50f, 0f);
            leftIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            var historyQuestBtnRect = tabs.FindWithDebug("Btn_History").GetComponent<RectTransform>();
            historyQuestBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            _historyQuestButton = historyQuestBtnRect.GetComponent<Button>();
            var rightIcon = UIStyle.DrawPadButtonIcon(historyQuestBtnRect, UIStyle.GamePadButton.Right);
            rightIcon.pivot = new Vector2(1f, 0.5f);
            rightIcon.anchorMin = new Vector2(1f, 0.5f);
            rightIcon.anchorMax = new Vector2(1f, 0.5f);
            rightIcon.anchoredPosition = new Vector2(-50f, 0f);
            rightIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            var sortBtnRect = transform.FindWithDebug("Content/Selection/SortingBar/Btn_Sort")
                .GetComponent<RectTransform>();
            sortBtnRect.gameObject.AddComponent<CanvasGroup>().interactable = false;
            _sortingButton = sortBtnRect.GetComponent<QuestSortButton>();
            var sortIcon = UIStyle.DrawPadButtonIcon(sortBtnRect, UIStyle.GamePadButton.Y);
            sortIcon.pivot = new Vector2(1f, 0.5f);
            sortIcon.anchorMin = new Vector2(1f, 0.5f);
            sortIcon.anchorMax = new Vector2(1f, 0.5f);
            sortIcon.anchoredPosition = new Vector2(-50f, 0f);
            sortIcon.sizeDelta = new Vector2(UIStyle.gamepad_btn_icon_size, UIStyle.gamepad_btn_icon_size);

            _leftScrollViewGamepadControl = transform.FindWithDebug("Content/Selection/Scroll View")
                .gameObject.AddComponent<ScrollViewGamepadControl>();
            _rightScrollViewGamepadControl = transform.FindWithDebug("Content/Details/Content/Scroll View")
                .gameObject.AddComponent<ScrollViewGamepadControl>();

            //Tips
            var tips = UIStyle.GamePadTipsRectTransform(transform.GetComponent<RectTransform>());
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.LeftAxisUpDownSlide,
                new[] { UIStyle.GamePadButton.LeftAxis });
            UIStyle.DrawPadButtonTips(tips, L10N.Instance.Exit,
                new[] { UIStyle.GamePadButton.Menu });
        }
    }
}
