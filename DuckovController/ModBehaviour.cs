using Duckov.MiniMaps.UI;
using Duckov.Quests.UI;
using Duckov.UI;
using Duckov.Utilities;
using DuckovController.Helper;
using LeTai.Asset.TranslucentImage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                // var hudManager = FindObjectOfType<HUDManager>();
                // var tran = hudManager.gameObject.transform;
                // while (tran.parent != null)
                // {
                //     tran = tran.parent; 
                // }
                // tran.transform.ShowAllComponents();
                var obj = FindObjectOfType<GameplayUIManager>();
                Debug.Log(obj == null);
                // var target = obj.transform.Find("LevelManager(Clone)/GameplayUICanvas/Tabs/ViewArea/MiniMapView/Content");
                var target = obj.GetComponentInChildren<QuestGiverView>(true);
                Debug.Log(target == null);
                var com = target.transform.GetChild(0) .GetComponent<TranslucentImage>();
                Debug.Log(com.spriteBlending);
                Debug.Log(com.vibrancy);
                Debug.Log(com.brightness);
                Debug.Log(com.flatten);
                Debug.Log(com.material.name);
                Debug.Log(com.color);
                Debug.Log(com.sprite);
                Debug.Log(com.source);

            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                var roots = SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (var root in roots)
                {
                    Debug.Log(root.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                //
                var hud = FindObjectOfType<HUDManager>();
                if (hud == null)
                {
                    Debug.LogError("hub is no found");
                }
                hud.GetComponent<RectTransform>().LogRectTransformInfo();
                var canvas = hud.gameObject.GetComponentInParent<Canvas>();
                if (canvas == null)
                {
                    Debug.LogError("canvas is no found");
                }
                canvas.GetComponent<RectTransform>().LogRectTransformInfo();
                var rect = canvas.GetComponent<CanvasScaler>();
                Debug.Log($"uiScaleMode {rect.uiScaleMode} {rect.referenceResolution}");
                Debug.Log($"uiScaleMode {rect.screenMatchMode} {rect.matchWidthOrHeight}");

                Debug.Log(canvas.gameObject.scene.name);
                if (canvas.transform.parent != null)
                {
                    // canvas.transform.parent.ShowAllComponents();
                }

                // canvas.transform.ShowAllComponents();
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                
                
                Utils.DebugShowAllSceneGameObject();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Utils.DebugShowAllDDOLSceneGameObject();
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
                PatchMainGame();
            }
        }
    }
}
