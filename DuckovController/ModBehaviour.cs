using Duckov.Utilities;
using DuckovController.Helper;
using DuckovController.SceneEdit.MainMenu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    //Entrance
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Awake");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                //
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                //
                InputSystem.DisableAllEnabledActions();
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
                if (!Utils.FindGameObject("Canvas", out Canvas canvas))
                {
                    Debug.LogError("找不到Canvas");
                    return;
                }
                canvas.gameObject.AddComponent<MainMenuOverride>();

                // MainMenuPatch = new MainMenuPatch();
                // MainMenuPatch.Patch1();
                // ResourceDataBase.Instance.Init(MainMenuPatch.MenuButtonListLayout);
                // MainMenuPatch.Patch2();
            }
        }
    }
}
