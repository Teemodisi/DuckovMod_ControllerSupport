using System.Reflection;
using Duckov.UI;
using Duckov.UI.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.SceneEdit.MainMenu
{
    public class MainMenuBtnButtonOverride : MonoBehaviour
    {
        private PunchReceiver _punchReceiver;

        private IPointerClickHandler[] _pointerClickHandlers;

        private FadeGroupButton _fadeGroupButton;

        private MainMenuOverride _mainMenuOverride;

        private void Awake()
        {
            _punchReceiver = GetComponent<PunchReceiver>();
            _pointerClickHandlers = gameObject.GetComponents<IPointerClickHandler>();
            _fadeGroupButton = GetComponent<FadeGroupButton>();
            _mainMenuOverride = GetComponentInParent<MainMenuOverride>();
        }

        public void Press()
        {
            if (_punchReceiver != null)
            {
                _punchReceiver.Punch();
            }
            foreach (var pointerClickHandler in _pointerClickHandlers)
            {
                pointerClickHandler.OnPointerClick(new PointerEventData(EventSystem.current)
                {
                    button = PointerEventData.InputButton.Left
                });
            }
            //当有子面板
            if (_fadeGroupButton != null)
            {
                var field = typeof(FadeGroupButton).GetField("openOnClick",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (field == null)
                {
                    Debug.LogError($"{nameof(MainMenuBtnButtonOverride)} 反射错误");
                    return;
                }
                // var panel = (field.GetValue(_fadeGroupButton) as FadeGroup)!.GetComponent<UIPanel>();
                // _mainMenuOverride.onCancelBtnDown += () => 
                // {
                //     if (gameObject.activeSelf)
                //     {
                //         panel.Close();
                //     }
                // };
                var panel = field.GetValue(_fadeGroupButton) as FadeGroup;
                var btn = panel.transform.Find("Return").GetComponentsInChildren<IPointerClickHandler>();
                _mainMenuOverride.onCancelBtnDown += () =>
                {
                    if (panel.IsShown)
                    {
                        foreach (var pointerClickHandler in btn)
                        {
                            pointerClickHandler.OnPointerClick(new PointerEventData(EventSystem.current)
                                { button = PointerEventData.InputButton.Left });
                        }
                    }
                };
            }
        }
    }
}
