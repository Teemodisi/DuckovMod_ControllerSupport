using DuckovController.Helper;
using DuckovController.SceneEdit.MainGame;
using DuckovController.SceneEdit.MainMenu;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController
{
    public partial class ModBehaviour
    {
        private void PatchMainMenu()
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

        private void PatchLoadingBlack()
        {
            // var ddolScene = gameObject.scene;
            // var rootObjects = ddolScene.GetRootGameObjects();
            // foreach (var rootObject in rootObjects)
            // {
            //     rootObject.transform.ShowAllComponents();
            // }

            var slObj = GameObject.Find("SceneLoader");
            if (slObj == null)
            {
                Debug.LogError("找不到 SceneLoader GameObject");
                return;
            }
            var interaction = slObj.transform.Find("Interaction");
            if (interaction == null)
            {
                Debug.LogError("找不到 SceneLoader/Interaction");
                return;
            }
            //SceneLoader/Interaction
            var loader = interaction.GetComponent<OnPointerClick>();
            if (loader == null)
            {
                Debug.LogError("找不到 SceneLoader/Interaction OnPointerClick");
                return;
            }
            loader.gameObject.AddComponent<SceneLoaderOverride>();
        }

        private void PatchMainGame()
        {
            PatchGameInput();
            PatchItemTurntableHUD();
        }

        private void PatchGameInput()
        {
            var playerInput = GameObject.Find("PlayerInput");
            if (playerInput == null)
            {
                Debug.LogError("找不到 PlayerInput");
                return;
            }
            var pi = playerInput.GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.LogError("找不到 PlayerInput Com");
                return;
            }
            pi.gameObject.AddComponent<MainGameInputOverride>();
        }

        private void PatchItemTurntableHUD()
        {
            var hudManager = FindObjectOfType<HUDManager>();
            var table = new GameObject("ItemTurntableHUD");
            table.transform.SetParent(hudManager.transform, false);
            table.AddComponent<MainGameItemTurntableHUD>();
        }
    }
}
