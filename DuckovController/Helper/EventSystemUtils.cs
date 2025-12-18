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

        public static void EmitEventPointerClick(this GameObject target, PointerEventData eventData)
        {
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void EmitEventPointerClickBtnLeft(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void EmitEventPointerDownBtnLeft(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerDownHandler);
        }

        public static void EmitEventPointerClickBtnRight(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Right
            };
            EmitEvent(target, eventData, ExecuteEvents.pointerClickHandler);
        }


        public static void EmitEventPointerClickAndDownBtnLeft(this GameObject target)
        {
            EmitEventPointerDownBtnLeft(target);
            EmitEventPointerClickBtnLeft(target);
        }

        public static void EmitEventPointerEnter(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current);
            EmitEvent(target, eventData, ExecuteEvents.pointerEnterHandler);
        }
        
        public static void EmitEventPointerExit(this GameObject target)
        {
            var eventData = new PointerEventData(EventSystem.current);
            EmitEvent(target, eventData, ExecuteEvents.pointerExitHandler);
        }
    }
}
