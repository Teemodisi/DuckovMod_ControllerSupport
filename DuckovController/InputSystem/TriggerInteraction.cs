using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DuckovController.InputSystem
{
    public class TriggerInteraction :IInputInteraction
    {
        public float deadZone;

        public void Process(ref InputInteractionContext context)
        {
            
        }

        public void Reset() { }
    }
}
