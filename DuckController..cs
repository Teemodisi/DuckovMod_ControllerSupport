using System.Collections.Generic;
using System.Linq;
using Duckov.UI;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Duckov Controller is loaded.");
        }

        protected override void OnAfterSetup()
        {
 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var canvas =  FindFirstObjectByType<Canvas>();
                Utils.ShowAllComponents(canvas.transform,showCom: true);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Utils.ShowHierarchy(true);
            }
        }

        private void ModTest()
        {
            var objs = SceneManager.GetActiveScene().GetRootGameObjects();


            // View.ActiveView


            // var coms = mainMenu.GetComponents<Component>();
            // foreach (var o in coms)
            // {
            //     Debug.Log($"{o.GetInstanceID()} {o.name} {o.GetType()}");
            // }

            // var menu = FindObjectOfType<Menu>();
            // Debug.Log("1");
            // Debug.Log(menu == null);
            // var items = typeof(Menu).GetField("items").GetValue(menu) as HashSet<MenuItem>;
            // Debug.Log("2");
            // Debug.Log(items?.Count);
        }

        protected override void OnBeforeDeactivate() { }
    }
}
