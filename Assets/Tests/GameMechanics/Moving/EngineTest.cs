using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Entities;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Moving
{

    public class EngineTest
    {
        private Engine _engine;

        private GameEntity _entity;
        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
            _engine = new Engine(_entity);
            _entity.Spawn();
        }

        [Test]
        public void Engine_rotates_entity()
        {
            _entity.RotationSpeed.Value = 1f;
            _engine.Tick(1f);
            Assert.Less(Mathf.Abs(_entity.Angle.Value - 1f), Mathf.Epsilon);
        }
        
        [Test]
        public void Engine_moves_entity()
        {
            _entity.Velocity.Value = new Vector2(1,1);
            _engine.Tick(1f);
            Assert.AreEqual(new Vector2(1,1), _entity.Position.Value);
        }
    }

}