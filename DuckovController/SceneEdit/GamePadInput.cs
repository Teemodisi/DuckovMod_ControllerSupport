using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit
{
    public class GamePadInput : MonoBehaviour
    {
        private void Update()
        {
            if (Gamepad.current.aButton.IsPressed())
            {
                
            }
        }
    }
}
