using UnityEngine;

namespace Asteroids.GameMechanics.Components.Input
{
    public class UnityInputWrapper : IKeyboardInput
    {
        public bool GetKey(KeyCode code)
        {
            return UnityEngine.Input.GetKey(code);
        }

        public bool GetKeyDown(KeyCode code)
        {
            return UnityEngine.Input.GetKeyDown(code);
        }
    }
}
