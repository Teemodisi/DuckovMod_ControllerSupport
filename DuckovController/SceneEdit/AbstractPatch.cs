using UnityEngine;
using UnityEngine.InputSystem;
using Utils = DuckovController.Helper.Utils;

namespace DuckovController.SceneEdit
{
    public abstract class AbstractPatch : MonoBehaviour
    {
        protected bool HasPatched { get; private set; }

        protected InputActionMap InputActionMap { get; private set; }

        protected virtual void Awake()
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} {GetType().Name} Awake");
#endif
            PatchImpl();
            InputActionMap = new InputActionMap(GetType().Name);
            InitInput(InputActionMap);
        }

        protected virtual void OnEnable()
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} {GetType().Name} OnEnable");
#endif
            EnableInputImpl();
        }

        protected virtual void OnDisable()
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} {GetType().Name} OnDisable");
#endif
            DisableInputImpl();
        }

        protected virtual void OnDestroy()
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} {GetType().Name} OnDestroy");
#endif
        }

        private void PatchImpl()
        {
            if (HasPatched)
            {
#if DEBUG
                Debug.Log($"{Utils.ModName} {GetType().Name} HasPatched pass.");
#endif
                return;
            }
            Patch();
            HasPatched = true;
        }

        private void EnableInputImpl()
        {
            InputActionMap.Enable();
            EnableInput();
        }

        private void DisableInputImpl()
        {
            InputActionMap.Disable();
            DisableInput();
        }

        /// <summary>
        /// 修改时机，在本 mono Awake 时调用
        /// </summary>
        protected virtual void Patch() { }

        /// <summary>
        /// 注册输入，绑定输入回调都在此处
        /// </summary>
        protected virtual void InitInput(InputActionMap inputActionMap) { }

        /// <summary>
        /// 除开 InputActionMap.Enable() 的特殊注册在这里添加
        /// </summary>
        protected virtual void EnableInput() { }

        /// <summary>
        /// 除开 InputActionMap.Disable() 的特殊注销在这里添加
        /// </summary>
        protected virtual void DisableInput() { }
    }
}
