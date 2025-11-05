using Duckov.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit
{
    public class StartupGamePadOverride:MonoBehaviour
    {
        private Startup _startup;
        private void Awake()
        {
            
        }

        public void Bind(Startup startup)
        {
            _startup = startup;
        }

        public void Update()
        {
            if (Gamepad.current.IsPressed())
            {
                // _startup.
            }
        }
    }
}
