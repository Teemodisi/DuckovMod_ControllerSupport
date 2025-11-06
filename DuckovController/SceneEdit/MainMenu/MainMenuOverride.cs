using DuckovController.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {
        private void Awake()
        {
            InitInputMap();
            Debug.Log($"{nameof(MainMenuOverride)} Awake");
        }

        private void Start()
        {
            Patch();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Utils.ShowAllComponents(MenuButtonListLayout, showCom: true);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var btns = MenuButtonListLayout.gameObject.GetComponentsInChildren<Button>();
                foreach (var button in btns)
                {
                    Debug.Log(button.navigation.mode);
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
