using UnityEngine;

namespace DuckovController.Helper
{
    public abstract class Singleton<T>  where T : new()
    {
        private static T s_Instance = default!;
        
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new T();
                }
                return s_Instance;
            }
        }
    }
    
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_Instance;

        public static T Instance
        {
            get
            {
                if (s_Instance is null)
                {
                    var find = FindObjectOfType<T>(true);
                    if (find != null)
                    {
                        s_Instance = find;
                    }
                    else
                    {
                        var obj = new GameObject("Singleton_" + nameof(T));
                        s_Instance = obj.AddComponent<T>();
                    }
                }
                return s_Instance;
            }
        }

        /// <summary>
        ///     Singleton:Don't forget the base.Awake
        /// </summary>
        protected virtual void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
#if UNITY_EDITOR
                Debug.LogWarning(
                    $"[SingletonMonoBehaviour] You've created the singleton repeatedly Type: <{typeof(T).FullName}>",
                    s_Instance);
#endif
                Destroy(s_Instance);
            }
            s_Instance = this as T;
        }

        /// <summary>
        ///     Singleton:Don't forget the base.OnDestroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            s_Instance = null;
        }
    }
}
