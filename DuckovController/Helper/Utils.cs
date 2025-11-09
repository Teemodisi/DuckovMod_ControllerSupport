using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckovController.Helper
{
    public static class Utils
    {
        private static readonly Type[] s_BaseExcludeList =
        {
            typeof(Transform),
            typeof(RectTransform),
            typeof(TMP_SubMeshUI),
            typeof(TMP_SubMesh),
            typeof(CanvasRenderer)
        };

        public static string ModName { get; } = "[Duckov Controller]";

        public static void ShowAllComponents(this Transform transform, bool showCom = true,
            IList<Type> excludeList = null)
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
            var stack = new Stack<Transform>();
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
                    LogGameObjectComponents(cur, sb, exclude);
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

        public static void LogGameObjectComponents(this Transform transform, StringBuilder sb,
            HashSet<Type> exclude = null)
        {
            var monos = transform.GetComponents<Component>();
            sb.Append('{');
            foreach (var monoBehaviour in monos)
            {
                var t = monoBehaviour.GetType();
                if (exclude != null && exclude.Contains(t))
                {
                    continue;
                }
                sb.Append(t.Name);
                sb.Append(", ");
            }
            sb.Append("}");
        }

        public static void LogGameObjectComponents(this Transform transform)
        {
            var sb = new StringBuilder();
            sb.Append(transform.gameObject.name);
            LogGameObjectComponents(transform, sb);
            Debug.Log(sb.ToString());
        }

        public static void ShowHierarchy(bool showCom = false)
        {
            var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var gameObject in gameObjects)
            {
                ShowAllComponents(gameObject.transform, showCom);
            }
        }

        public static bool FindGameObject<T>(string path, out T com)
        {
            var gameObject = GameObject.Find(path);
            if (gameObject != null)
            {
                return gameObject.TryGetComponent(out com);
            }
            com = default!;
            return false;
        }

        public static void LogRectTransformInfo(this RectTransform rectTransform)
        {
            Debug.Log($"[RectTransform INFO]{rectTransform.name}");
            Debug.Log($"{nameof(rectTransform.anchorMin)}: {rectTransform.anchorMin}");
            Debug.Log($"{nameof(rectTransform.anchorMax)}: {rectTransform.anchorMax}");
            Debug.Log($"{nameof(rectTransform.pivot)}: {rectTransform.pivot}");
            Debug.Log($"{nameof(rectTransform.offsetMin)}: {rectTransform.offsetMin}");
            Debug.Log($"{nameof(rectTransform.offsetMax)}: {rectTransform.offsetMax}");
            Debug.Log($"{nameof(rectTransform.anchoredPosition)}: {rectTransform.anchoredPosition}");
            Debug.Log($"{nameof(rectTransform.sizeDelta)}: {rectTransform.sizeDelta}");
        }

        public static Type FindType(string name)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
