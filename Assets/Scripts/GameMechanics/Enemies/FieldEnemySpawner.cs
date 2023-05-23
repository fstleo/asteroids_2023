using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{
    
    /// <summary>
    /// Creates enemy and puts it on field
    /// </summary>
    public class FieldEnemySpawner : IEnemyCreator
    {
        private readonly IAsteroidField _asteroidField;
        private readonly IFactory<EnemyType, GameEntity> _enemyFactory;

        public FieldEnemySpawner(IAsteroidField asteroidField, IFactory<EnemyType, GameEntity> enemyFactory)
        {
            _asteroidField = asteroidField;
            _enemyFactory = enemyFactory;
        }
        
        private void RemoveFromField(GameEntity asteroid)
        {
            _asteroidField.RemoveEnemy(asteroid);
        }

        public void PutOnField(EnemyType enemy)
        {
            PutOnField(enemy, _asteroidField.GetRandomPoint());
        }

        private void PutOnField(EnemyType enemyType, Vector2 position)
        {
            var enemy = _enemyFactory.Create(enemyType);
            enemy.Position.Value = position;
            
            
            // TODO: replace with additional setup class?
            switch (enemyType)
            {
                case EnemyType.LargeAsteroid:

                    void SpawnMediumLeftovers()
                    {
                        enemy.Died -= SpawnMediumLeftovers;
                        PutOnField(EnemyType.MediumAsteroid, enemy.Position.Value);
                        PutOnField(EnemyType.MediumAsteroid, enemy.Position.Value);
                    }

                    enemy.Died += SpawnMediumLeftovers;
                    break;
                case EnemyType.MediumAsteroid:

                    void SpawnSmallLeftovers()
                    {
                        enemy.Died -= SpawnSmallLeftovers;
                        PutOnField(EnemyType.SmallAsteroid, enemy.Position.Value);
                        PutOnField(EnemyType.SmallAsteroid, enemy.Position.Value);
                    }

                    enemy.Died += SpawnSmallLeftovers;
                    break;
            }

            void EnemyDied()
            {
                enemy.Died -= EnemyDied;
                RemoveFromField(enemy);
            }

            enemy.Died += EnemyDied;
            _asteroidField.AddEnemy(enemy);
        }

    }
}
