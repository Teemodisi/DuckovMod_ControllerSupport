using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController
{
    public static class Utils
    {
        private static readonly Type[] s_BaseExcludeList = new[]
        {
            typeof(Transform),
            typeof(RectTransform),
            typeof(TMP_SubMeshUI),
            typeof(TMP_SubMesh),
            typeof(CanvasRenderer)
        };
        
        public static void ShowAllComponents(Transform transform, IList<Type>? excludeList = null, bool showCom = false)
        {
            var exclude = new HashSet<Type>();
            foreach (var type in s_BaseExcludeList)
            {
                exclude.Add(type);
            }
            if (excludeList != null)
            {
                foreach (var type in excludeList)
                {
                    exclude.Add(type);
                }
            }
            var stack = new Stack<Transform?>();
            stack.Push(transform);
            var level = 0;
            var sb = new StringBuilder();
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                if (cur == null)
                {
                    level--;
                    continue;
                }
                sb.Clear();
                for (var i = 0; i < level; i++)
                {
                    sb.Append("  ");
                }
                sb.Append(cur.gameObject.name);

                if (showCom)
                {
                    var monos = cur.GetComponents<Component>();
                    sb.Append('{');
                    foreach (var monoBehaviour in monos)
                    {
                        var t = monoBehaviour.GetType();
                        if (exclude.Contains(t))
                        {
                            continue;
                        }
                        sb.Append(t.Name);
                        sb.Append(", ");
                    }
                    sb.Append("}");
                }

                Debug.Log(sb.ToString());

                level++;
                stack.Push(null);
                var count = cur.childCount;
                for (var i = count - 1; i >= 0; i--)
                {
                    stack.Push(cur.GetChild(i));
                }
            }
        }

        public static void ShowHierarchy(bool showCom = false)
        {
            var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var gameObject in gameObjects)
            {
                ShowAllComponents(gameObject.transform, showCom: showCom);
            }
        }
    }
}
