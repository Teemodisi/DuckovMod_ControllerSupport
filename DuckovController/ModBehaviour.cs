using Duckov.Utilities;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    //Mod Main Entrance
    public partial class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Awake");
            gameObject.AddComponent<GamePadInput>();
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
                PatchMainMenu();
                return;
            }
            if (scene.name == SceneLoader.Instance.defaultCurtainScene.Name)
            {
                //Scene name: LoadingScreen_Black
                PatchLoadingBlack();
            }
            if (scene.name == GameplayDataSettings.SceneManagement.BaseScene.Name)
            {
                PatchMainGame();
            }
        }
    }
}
