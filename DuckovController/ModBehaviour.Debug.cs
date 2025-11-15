using System.Linq;
using System.Reflection;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DuckovController
{
    public partial class ModBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var inputManager = GameObject.FindObjectOfType<LevelManager>();
                inputManager.transform.ShowAllComponents(depth: 3);
                // var fields = typeof(LevelManager)
                //     .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                //     .Where(x => x.Name.Contains("inputAction"));
                // foreach (var field in fields)
                // {
                //     Debug.Log($"=={field.Name}==");
                //     var ia = (InputAction)field.GetValue(UIInputManager.Instance);
                //     foreach (var inputBinding in ia.bindings)
                //     {
                //         Debug.Log(inputBinding.path);
                //     }
                // }
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
    }
}
