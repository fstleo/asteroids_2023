using System.Linq;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics
{
    public class AsteroidFieldTests
    {
        private readonly Bounds2D _bounds = new(Vector2.zero, new Vector2(12, 12));
        private AsteroidField _asteroidField;
        
        [SetUp]
        public void Setup()
        {
            _asteroidField = new AsteroidField(_bounds);
        }
        
        [Test]
        public void Bounds_from_constructor()
        {
            Assert.AreEqual(_bounds, _asteroidField.Bounds);
        }
        
        [Test]
        public void Random_point_from_bounds()
        {
            Assert.IsTrue(_bounds.Contains(_asteroidField.GetRandomPoint()));
        }

        [Test]
        public void Player_added_event_called_and_player_set()
        {
            var playerEntity = new GameEntity();

            GameEntity playerFromEvent = null;
            _asteroidField.PlayerCreated += player => playerFromEvent = player;
            
            _asteroidField.AddPlayer(playerEntity);
            
            Assert.AreSame(playerEntity, playerFromEvent);
            Assert.AreSame(playerEntity, _asteroidField.Player);
        }
        
        [Test]
        public void Asteroid_added_to_list_and_event_called()
        {
            var asteroidEntity = new GameEntity();

            GameEntity asteroidFromEvent = null;
            _asteroidField.EnemyCreated += asteroid =>
            {
                asteroidFromEvent = asteroid;
            };
                
            
            _asteroidField.AddEnemy(asteroidEntity);
            Assert.IsTrue(_asteroidField.Enemies.Contains(asteroidEntity));
            Assert.AreSame(asteroidEntity, asteroidFromEvent);
        }
        
        [Test]
        public void Asteroid_removed_from_list_and_event_called()
        {
            var asteroidEntity = new GameEntity();

            _asteroidField.AddEnemy(asteroidEntity);
            
            GameEntity asteroidFromEvent = null;
            _asteroidField.EnemyDestroyed += asteroid =>
            {
                asteroidFromEvent = asteroid;
            };
                
            
            _asteroidField.RemoveEnemy(asteroidEntity);
            Assert.IsFalse(_asteroidField.Enemies.Contains(asteroidEntity));
            Assert.AreSame(asteroidEntity, asteroidFromEvent);
        }
    }


}
