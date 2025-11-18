using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.InputSystemExtend
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public class RepeatInteraction : IInputInteraction
    {
        public float initialDelay = 0.5f;

        public float repeatRate = 0.1f;

        public bool triggerOnStart = true;

        private bool _hasTriggeredInitial;

        private float _holdTime;

        private bool _isHolding;

#if UNITY_EDITOR
        static RepeatInteraction()
        {
            Initialize();
        }
#endif

        public void Process(ref InputInteractionContext context)
        {
            if (context.timerHasExpired)
            {
                if (_isHolding)
                {
                    context.PerformedAndStayStarted();
                    context.SetTimeout(repeatRate);
                }
                return;
            }
            switch (context.phase)
            {
                case InputActionPhase.Waiting:
                    if (context.ControlIsActuated())
                    {
                        context.Started();
                        _holdTime = 0f;
                        _isHolding = true;
                        _hasTriggeredInitial = false;
                        if (triggerOnStart)
                        {
                            context.PerformedAndStayStarted();
                            _hasTriggeredInitial = true;
                        }

                        context.SetTimeout(initialDelay);
                    }
                    break;

                case InputActionPhase.Started:
                    _holdTime += Time.deltaTime;
                    if (!context.ControlIsActuated())
                    {
                        context.Canceled();
                        _isHolding = false;
                        _hasTriggeredInitial = false;
                        return;
                    }
                    if (_holdTime >= initialDelay && !_hasTriggeredInitial)
                    {
                        context.PerformedAndStayStarted();
                        context.SetTimeout(repeatRate);
                        _hasTriggeredInitial = true;
                    }
                    break;
                case InputActionPhase.Performed:
                    if (!context.ControlIsActuated())
                    {
                        context.Canceled();
                        _isHolding = false;
                        _hasTriggeredInitial = false;
                    }
                    break;
            }
        }

        public void Reset()
        {
            _isHolding = false;
            _holdTime = 0f;
            _hasTriggeredInitial = false;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            InputSystem.RegisterInteraction<RepeatInteraction>("Repeat");
        }
    }
}
