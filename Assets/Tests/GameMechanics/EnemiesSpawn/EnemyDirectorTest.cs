using System;
using System.Collections.Generic;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Enemies;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.EnemiesSpawn
{

    public class EnemyDirectorTest
    {
        private EnemyWavesGenerator _enemyDirector;
        private IAsteroidField _asteroidField;
        private IEnemyWavesSettings _enemyWavesSettings;
        private IFactory<GameEntity, EnemyType> _asteroidsFactory;
        private IEnemyCreator _enemyCreator; 
        
        [SetUp]
        public void Setup()
        {
            _enemyCreator = Substitute.For<IEnemyCreator>();
            _asteroidField = Substitute.For<IAsteroidField>();
            _enemyWavesSettings = Substitute.For<IEnemyWavesSettings>();
            _enemyWavesSettings.GetWave(0).Returns(new EnemyWave
                {
                    Enemies = new[]
                    {
                        new EnemyWave.Enemy
                        {
                            Type = EnemyType.LargeAsteroid,
                            Count = 2
                        },
                        new EnemyWave.Enemy
                        {
                            Type = EnemyType.MediumAsteroid,
                            Count = 3
                        },
                        new EnemyWave.Enemy
                        {
                            Type = EnemyType.SmallAsteroid,
                            Count = 1
                        },
                    }
                }
            );
            
            _enemyWavesSettings.GetWave(1).Returns(new EnemyWave
            {
                Enemies = new []
                {
                    new EnemyWave.Enemy
                    {
                        Type = EnemyType.LargeAsteroid,
                        Count = 4
                    },
                    new EnemyWave.Enemy
                    {
                        Type = EnemyType.MediumAsteroid,
                        Count = 3
                    },
                    new EnemyWave.Enemy
                    {
                        Type = EnemyType.SmallAsteroid,
                        Count = 2
                    },
                }
            });
            
            _enemyDirector = new EnemyWavesGenerator(_asteroidField, _enemyWavesSettings, _enemyCreator);
        }

        [Test]
        public void Spawns_wave_according_to_settings()
        {
            _enemyDirector.SpawnNextWave();

            _enemyCreator.Received(2).PutOnField(EnemyType.LargeAsteroid);
            _enemyCreator.Received(3).PutOnField(EnemyType.MediumAsteroid);
            _enemyCreator.Received(1).PutOnField(EnemyType.SmallAsteroid);
        }
        
        [Test]
        public void New_wave_when_all_enemies_destroyed()
        {
            var asteroid = new GameEntity();
            _asteroidField.EnemyCreated += Raise.Event<Action<GameEntity>>(asteroid);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(asteroid);
            
            _enemyCreator.Received(4).PutOnField(EnemyType.LargeAsteroid);
            _enemyCreator.Received(3).PutOnField(EnemyType.MediumAsteroid);
            _enemyCreator.Received(2).PutOnField(EnemyType.SmallAsteroid);
        }
        
        [Test]
        public void Do_not_spawn_wave_when_there_are_enemies()
        {
            var asteroid = new GameEntity();
            
            _asteroidField.Enemies.Returns(new List<GameEntity> { asteroid });
            
            _asteroidField.EnemyCreated += Raise.Event<Action<GameEntity>>(asteroid);
            _asteroidField.EnemyCreated += Raise.Event<Action<GameEntity>>(asteroid);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(asteroid);
            
            
            _enemyCreator.Received(0).PutOnField(EnemyType.LargeAsteroid);
            _enemyCreator.Received(0).PutOnField(EnemyType.MediumAsteroid);
            _enemyCreator.Received(0).PutOnField(EnemyType.SmallAsteroid);
        }
    }
}