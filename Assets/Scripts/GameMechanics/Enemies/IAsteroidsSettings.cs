using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{
    public interface IAsteroidsSettings
    {
        Vector2 GetRandomVelocity();
        float GetRandomRotationSpeed();
        IEnumerable<AsteroidPrefab> GetPrefabsIds();
    }
}