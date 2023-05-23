using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.GameMechanics.Enemies
{
    [Serializable]
    public class AsteroidsSettings : IAsteroidsSettings
    {
        public float MinimumSpeed;
        public float MaximumSpeed;
        public float MinimumRotationSpeed;
        public float MaximumRotationSpeed;
        
        public AsteroidPrefab[] Prefabs;

        public IEnumerable<AsteroidPrefab> GetPrefabsIds()
        {
            return Prefabs;
        }

        public Vector2 GetRandomVelocity()
        {
            return Random.insideUnitCircle.normalized * Random.Range(MinimumSpeed, MaximumSpeed);
        }

        public float GetRandomRotationSpeed()
        {
            return Random.Range(MinimumRotationSpeed, MaximumRotationSpeed);
        }
    }
}