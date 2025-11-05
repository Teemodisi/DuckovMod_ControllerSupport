using Duckov.Utilities;
using DuckovController.SceneEdit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    //Entrance
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public MainMenuPatch MainMenuPatch { get; private set; } = null!;

        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Awake");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Utils.Utils.ShowHierarchy(true);z
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                //
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                //
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                //
            }
        }

        protected override void OnAfterSetup()
        {
            SceneManager.sceneLoaded += OnSceneLoad;

            Debug.Log($"[{nameof(DuckovController)}] OnAfterSetup");
        }

        protected override void OnBeforeDeactivate()
        {
            SceneManager.sceneLoaded -= OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == GameplayDataSettings.SceneManagement.MainMenuScene.Name)
            {
                InputManager.OnInputDeviceChanged += InputManagerOnOnInputDeviceChanged;
                
                MainMenuPatch = new MainMenuPatch();
                MainMenuPatch.Patch1();
                ResourceDataBase.Instance.Init(MainMenuPatch.MenuButtonListLayout);
                MainMenuPatch.Patch2();
            }
        }

        private void InputManagerOnOnInputDeviceChanged()
        {
        }
    }
}
