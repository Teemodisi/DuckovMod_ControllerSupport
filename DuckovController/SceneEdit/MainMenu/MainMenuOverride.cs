using Duckov.UI.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {
        private MainMenuBtnButtonOverride[] _buttons;

        private FadeGroup _fadeGroup;

        private void Awake()
        {
            _fadeGroup = GetComponent<FadeGroup>();
            _fadeGroup.OnShowComplete += OnFadeGroupCompleted;
        }

        private void Start()
        {
            Patch();
            _buttons = new MainMenuBtnButtonOverride[MenuButtonListLayout.childCount];
            for (var i = 0; i < _buttons.Length; i++)
            {
                _buttons[i] = MenuButtonListLayout.GetChild(i).gameObject.AddComponent<MainMenuBtnButtonOverride>();
            }
            //选中第一
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
        }

        private void OnEnable()
        {
            RegInput();
        }

        private void OnDisable()
        {
            UnRegInput();
        }
    }
}
