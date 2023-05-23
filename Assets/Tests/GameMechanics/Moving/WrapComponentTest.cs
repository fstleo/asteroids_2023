using Asteroids.GameMechanics.Entities;
using GameMechanics.Components.WrapPosition;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Moving
{
    public class WrapComponentTest
    {
        private GameEntity _entity;
        private WrapPosition _wrapPosition;

        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
            _wrapPosition = new WrapPosition(_entity, new Bounds2D(Vector2.zero, new Vector2(40,40)));
        }
        
        
        [Test]
        public void Teleport_to_the_right_if_less_then_bounds_min_x()
        {
            _entity.Position.Value = new Vector2(-21, 0);
            
            _wrapPosition.Tick(0.01f);
            
            Assert.AreEqual(new Vector2(19, 0),_entity.Position.Value);
        }
        
        [Test]
        public void Teleport_to_the_right_if_more_then_bounds_max_x()
        {
            _entity.Position.Value = new Vector2(21, 0);
            
            _wrapPosition.Tick(0.01f);
            
            Assert.AreEqual(new Vector2(-19, 0),_entity.Position.Value);
        }
        
        [Test]
        public void Teleport_to_the_top_if_lower_then_bounds_min_y()
        {
            _entity.Position.Value = new Vector2(0, -21);
            
            _wrapPosition.Tick(0.01f);
            
            Assert.AreEqual(new Vector2(0, 19),_entity.Position.Value);
        }

        [Test]
        public void Teleport_to_the_top_if_higher_then_bounds_max_y()
        {
            _entity.Position.Value = new Vector2(0,21);
            
            _wrapPosition.Tick(0.01f);
            
            Assert.AreEqual(new Vector2(0, -19),_entity.Position.Value);
        }

    }
}
