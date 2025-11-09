using Duckov.Utilities;
using DuckovController.Helper;
using DuckovController.SceneEdit;
using DuckovController.SceneEdit.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    //Entrance
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Awake");
            gameObject.AddComponent<GamePadInput>();
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
#if DEBUG
            Debug.Log($"{Utils.ModName} SceneLoadListen: {scene.name}");
#endif
            if (scene.name == GameplayDataSettings.SceneManagement.MainMenuScene.Name)
            {
                if (!Utils.FindGameObject("Canvas", out Canvas canvas))
                {
                    Debug.LogError("找不到 Canvas");
                    return;
                }
                var mainTitle = canvas.transform.Find("MainTitle");
                if (mainTitle == null)
                {
                    Debug.LogError("找不到主菜单的 MainTitle");
                    return;
                }
                mainTitle.gameObject.AddComponent<MainTitleOverride>();
                var mainGroup = canvas.transform.Find("MainMenuContainer/Menu/MainGroup");
                if (mainGroup == null)
                {
                    Debug.LogError("找不到主菜单的 MainGroup");
                    return;
                }
                mainGroup.gameObject.AddComponent<MainMenuOverride>();
            }
        }
    }
}
