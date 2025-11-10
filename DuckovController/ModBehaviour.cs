using Duckov.UI;
using Duckov.Utilities;
using DuckovController.Helper;
using TMPro;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                //
                BlackScreen.Instance.gameObject.SetActive(!BlackScreen.Instance.gameObject.activeSelf);
                Debug.Log(BlackScreen.Instance.gameObject.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                //
                SceneManager.LoadScene("LoadingScreen_Black", LoadSceneMode.Single);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                //
                var roots = SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (var root in roots)
                {
                    Debug.Log("==========");
                    root.transform.ShowAllComponents();
                    if (root.TryGetComponent(out TextMeshPro tmp))
                    {
                        Debug.Log($"TMP!: {tmp.text}");
                    }
                }
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
                PatchGameInput();
            }
        }
    }
}
