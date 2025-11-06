using DuckovController.Helper;
using UnityEngine;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {
        private MainMenuBtnButtonOverride[] _buttons;

        private void Awake()
        {
            InitInputMap();
            Debug.Log($"{nameof(MainMenuOverride)} Awake");
        }

        private void Start()
        {
            Patch();
            _buttons = new MainMenuBtnButtonOverride[MenuButtonListLayout.childCount];
            for (var i = 0; i < _buttons.Length; i++)
            {
                _buttons[i] = MenuButtonListLayout.GetChild(i).gameObject.AddComponent<MainMenuBtnButtonOverride>();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Utils.ShowAllComponents(MenuButtonListLayout, showCom: true);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                foreach (var actionsBinding in GameManager.MainPlayerInput.actions.bindings)
                {
                    Debug.Log($"{actionsBinding.action}, {actionsBinding.path}");
                }
            }
        }

        private void OnEnable()
        {
            _inputActionMap.Enable();
            Debug.Log($"{nameof(MainMenuOverride)} OnEnable");
        }

        private void OnDisable()
        {
            _inputActionMap.Disable();
            Debug.Log($"{nameof(MainMenuOverride)} OnDisable");
        }
    }
}
