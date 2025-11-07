using DuckovController.Helper;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {
        private MainMenuBtnButtonOverride[] _buttons;

        private void Awake()
        {
            InitInputMap();
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
            _inputActionMap.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap.Disable();
        }
    }
}
