using System;
using UnityEngine;

namespace DuckovController
{
    public class DuckovController : Duckov.Modding.ModBehaviour
    {
        private void Awake()
        {
            Debug.Log($"[{nameof(DuckovController)}] Duckov Controller is loaded.");
            
            
        }
    }
}
