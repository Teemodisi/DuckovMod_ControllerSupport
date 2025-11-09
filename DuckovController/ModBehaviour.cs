using System.Linq;
using System.Reflection;
using Duckov.Utilities;
using DuckovController.Helper;
using DuckovController.SceneEdit.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    //Entrance
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private GameObject _gameObject1;

        private GameObject _gameObject2;

        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Awake");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var root = SceneManager.GetActiveScene().GetRootGameObjects();
                _gameObject1 =  root.First(x => x.name == "Art");
                _gameObject1 =  root.First(x => x.name == "Terrain");
                
                // foreach (var o in root)
                // {
                //     Debug.Log(o.name);
                //     // o.transform.ShowAllComponents();
                // }
                // if (!Utils.FindGameObject("TimelineContent", out Transform target))
                // {
                //     Debug.LogError("找不到 target");
                //     return;
                // }
                // var ak = target.Find("LOGO/PressAnyKeyContinue");
                // if (ak == null)
                // {
                //     Debug.LogError("找不到 PressAnyKeyContinue");
                //     return;
                // }
                // ak.GetComponent<RectTransform>().DebugRect(new Color( 1, 1, 1,0.1f));
                // Debug.Log("nihao ");//错误的
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                _gameObject1.SetActive(!_gameObject1.activeSelf);
                Debug.Log("F3");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                _gameObject2.SetActive(!_gameObject2.activeSelf);
                Debug.Log("F4");
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
                // var mainGroup = canvas.transform.Find("MainMenuContainer/Menu/MainGroup");
                // if (mainGroup == null)
                // {
                //     Debug.LogError("找不到主菜单的 MainGroup");
                //     return;
                // }
                // mainGroup.gameObject.AddComponent<MainMenuOverride>();
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
