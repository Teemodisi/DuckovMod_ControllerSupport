using System.Reflection;
using Duckov.UI;
using Duckov.UI.Animations;
using Duckov.UI.MainMenu;
using DuckovController.Helper;
using UnityEngine;

namespace DuckovController.SceneEdit.MainMenu
{
    //TODO:删除存档交互还没做
    public class MainMenuBtnButtonOverride : MonoBehaviour
    {
        private PunchReceiver _punchReceiver;

        private void Awake()
        {
            _punchReceiver = GetComponent<PunchReceiver>();
            TryPatchReturnButton();
        }

        private void TryPatchReturnButton()
        {
            var saveButton = GetComponent<SavesButton>();
            if (saveButton != null)
            {
                PatchSavePanel(saveButton);
                return;
            }

            var fadeGroupButton = GetComponent<FadeGroupButton>();
            //当有子面板
            if (fadeGroupButton != null)
            {
                var field = typeof(FadeGroupButton).GetField("openOnClick",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (field == null)
                {
                    Debug.LogError($"{nameof(MainMenuBtnButtonOverride)} 反射错误");
                    return;
                }
                var panel = field.GetValue(fadeGroupButton) as FadeGroup;
                if (panel != null)
                {
                    var returnBtn = panel.transform.Find("Return");
                    if (returnBtn == null)
                    {
                        return;
                    }
                    returnBtn.EmitEventPointerClickAndDownBtnLeft();
                }
            }
        }

        private void PatchSavePanel(SavesButton savesButton)
        {
            //TODO:有空再覆盖这个面板
            var menu = typeof(SavesButton).GetField("selectionMenu", BindingFlags.NonPublic | BindingFlags.Instance)
                !.GetValue(savesButton) as SaveSlotSelectionMenu;
            var btn = menu?.transform.Find("Cancel");
            btn.EmitEventPointerClickAndDownBtnLeft();
        }

        public void Press()
        {
            if (_punchReceiver != null)
            {
                _punchReceiver.Punch();
            }
            gameObject.EmitEventPointerClickAndDownBtnLeft();
        }
    }
}
