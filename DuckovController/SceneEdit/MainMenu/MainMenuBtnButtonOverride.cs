using System.Reflection;
using Duckov.UI;
using Duckov.UI.Animations;
using Duckov.UI.MainMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.SceneEdit.MainMenu
{
    //TODO:删除存档交互还没做
    public class MainMenuBtnButtonOverride : MonoBehaviour
    {
        private PunchReceiver _punchReceiver;

        private IPointerClickHandler[] _openFadeGroupHandlers;

        private MainMenuOverride _mainMenuOverride;

        private PointerEventData MouseLeftClick { get; } = new PointerEventData(EventSystem.current)
            { button = PointerEventData.InputButton.Left };

        private void Awake()
        {
            _punchReceiver = GetComponent<PunchReceiver>();
            _openFadeGroupHandlers = gameObject.GetComponents<IPointerClickHandler>();
            _mainMenuOverride = GetComponentInParent<MainMenuOverride>();
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
                    var btn = returnBtn.GetComponentsInChildren<IPointerClickHandler>();
                    //添加返回监听
                    _mainMenuOverride.onCancelBtnDown += () =>
                    {
                        if (panel.IsShown)
                        {
                            foreach (var pointerClickHandler in btn)
                            {
                                pointerClickHandler.OnPointerClick(MouseLeftClick);
                            }
                        }
                    };
                }
            }
        }

        private void PatchSavePanel(SavesButton savesButton)
        {
            //TODO:有空再覆盖这个面板
            var menu = typeof(SavesButton).GetField("selectionMenu", BindingFlags.NonPublic | BindingFlags.Instance)
                !.GetValue(savesButton) as SaveSlotSelectionMenu;
            var group = typeof(SaveSlotSelectionMenu).GetField("fadeGroup",
                    BindingFlags.NonPublic | BindingFlags.Instance)
                !.GetValue(menu) as FadeGroup;
            var btn = menu?.transform.Find("Cancel");
            var allClick = btn!.GetComponentsInChildren<IPointerClickHandler>();
            _mainMenuOverride.onCancelBtnDown += () =>
            {
                if (group != null && group.IsShown)
                {
                    foreach (var pointerClickHandler in allClick)
                    {
                        pointerClickHandler.OnPointerClick(MouseLeftClick);
                    }
                }
            };
        }

        public void Press()
        {
            if (_punchReceiver != null)
            {
                _punchReceiver.Punch();
            }
            foreach (var pointerClickHandler in _openFadeGroupHandlers)
            {
                pointerClickHandler.OnPointerClick(new PointerEventData(EventSystem.current)
                {
                    button = PointerEventData.InputButton.Left
                });
            }
        }
    }
}
