using System;

namespace Asteroids.GameMechanics.Components.Moving
{

    [Serializable]
    public class AccelerationSettings
    {
        public float MaximumSpeed;
        public float Acceleration;
        public float RotationSpeed;
    }
}