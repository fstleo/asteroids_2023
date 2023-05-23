using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Enemies
{

    /// <summary>
    /// Spawns enemies if field is empty
    /// </summary>
    public class EnemyWavesGenerator
    {
        private readonly IAsteroidField _asteroidField;
        private readonly IEnemyWavesSettings _enemyWavesSettings;
        private readonly IEnemyCreator _enemySpawner;
        
        private int _currentWave;

        public EnemyWavesGenerator(IAsteroidField asteroidField, IEnemyWavesSettings enemyWavesSettings, IEnemyCreator enemyCreator)
        {
            _asteroidField = asteroidField;
            _enemyWavesSettings = enemyWavesSettings;
            _enemySpawner = enemyCreator;
            asteroidField.EnemyDestroyed += DecreaseCurrentEnemiesCount;
        }
        
        private void DecreaseCurrentEnemiesCount(GameEntity gameEntity)
        {
            if (_asteroidField.Enemies.Count > 0)
            {
                return;
            }
            
            _currentWave++;
            SpawnNextWave();
        }

        public void SpawnNextWave()
        {
            var currentWave = _enemyWavesSettings.GetWave(_currentWave);
            foreach (var enemyType in currentWave.Enemies)
            {
                for (var i = 0; i < enemyType.Count; i++)
                {
                    _enemySpawner.PutOnField(enemyType.Type);
                }
            }
        }
        

    }
}