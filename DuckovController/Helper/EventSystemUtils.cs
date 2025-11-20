using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.Helper
{
    public static class EventSystemUtils
    {
        public static void EmitEvent<T>(
            this GameObject target,
            BaseEventData eventData,
            ExecuteEvents.EventFunction<T> functor)
            where T : IEventSystemHandler
        {
            ExecuteEvents.Execute(target, eventData, functor);
        }

        public static void EmitEvent<T>(
            this Component target,
            BaseEventData eventData,
            ExecuteEvents.EventFunction<T> functor)
            where T : IEventSystemHandler
        {
            EmitEvent(target.gameObject, eventData, functor);
        }

        public static void EmitEventPointerClick(this GameObject target, PointerEventData eventData)
        {
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void EmitEventPointerClick(this Component target, PointerEventData eventData)
        {
            EmitEventPointerClick(target.gameObject, eventData);
        }

        public static void EmitEventPointerClickBtnLeft(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void EmitEventPointerClickBtnLeft(this Component target)
        {
            EmitEventPointerClickBtnLeft(target.gameObject);
        }

        public static void EmitEventPointerDownBtnLeft(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerDownHandler);
        }

        public static void EmitEventPointerDownBtnLeft(this Component target)
        {
            EmitEventPointerDownBtnLeft(target.gameObject);
        }

        public static void EmitEventPointerClickBtnRight(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Right
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void EmitEventPointerClickBtnRight(this Component target)
        {
            EmitEventPointerClickBtnRight(target.gameObject);
        }

        public static void EmitEventPointerClickAndDownBtnLeft(this GameObject target)
        {
            EmitEventPointerDownBtnLeft(target);
            EmitEventPointerClickBtnLeft(target);
        }

        public static void EmitEventPointerClickAndDownBtnLeft(this Component target)
        {
            EmitEventPointerClickAndDownBtnLeft(target.gameObject);
        }
    }
}
