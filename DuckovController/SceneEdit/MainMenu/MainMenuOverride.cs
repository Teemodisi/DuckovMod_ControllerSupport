using DuckovController.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {
        private Button[] _buttons;

        private void Awake()
        {
            InitInputMap();
            Debug.Log($"{nameof(MainMenuOverride)} Awake");
        }

        private void Start()
        {
            Patch();
            _buttons = new Button[MenuButtonListLayout.childCount];
            for (var i = 0; i < _buttons.Length; i++)
            {
                _buttons[i] = gameObject.GetComponent<Button>();
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
                Debug.Log(gameObject.GetComponentsInChildren<Button>().Length);
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
