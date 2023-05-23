using System.Collections.Generic;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Enemies
{

    /// <summary>
    /// Builds enemy entities
    /// </summary>
    public class EnemySpawner : IFactory<EnemyType, GameEntity>
    {
        private readonly Dictionary<EnemyType, IFactory<GameEntity>> _enemiesFactories = new(4);
        private readonly IAsteroidsSettings _settings;
        
        public EnemySpawner(IFactory<string, EntityBuilder> defaultEntity, IAsteroidsSettings settings)
        {
            _settings = settings;
            foreach (var prefabId in settings.GetPrefabsIds())
            {
                _enemiesFactories.Add(prefabId.Type, defaultEntity.Create(prefabId.PrefabId));    
            }
        }
        
        public GameEntity Create(EnemyType enemyType)
        {
            var asteroid = _enemiesFactories[enemyType].Create();
            asteroid.Velocity.Value = _settings.GetRandomVelocity();
            asteroid.RotationSpeed.Value = _settings.GetRandomRotationSpeed();
            return asteroid;
        }

    }
}