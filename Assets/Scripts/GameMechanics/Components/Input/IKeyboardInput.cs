using UnityEngine;

namespace Asteroids.GameMechanics.Components.Input
{
    public interface IKeyboardInput
    {
        bool GetKey(KeyCode code);
        bool GetKeyDown(KeyCode code);
    }
}
