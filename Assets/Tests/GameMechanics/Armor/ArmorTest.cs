using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Entities;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Armor
{

    public class ArmorComponentTest
    {
        private IArmor _armor;
        private ArmorComponent _armorComponent;
        
        [SetUp]
        public void Setup()
        {
            _armor = Substitute.For<IArmor>();
            
            _armorComponent = new GameObject().AddComponent<ArmorComponent>();

        }
        
        [Test]
        public void Armor_collision_doesnt_if_not_inited()
        {
            _armorComponent.OnCollisionEnter2D(new Collision2D());
            
            _armor.DidNotReceive().Hit();
        }


        [Test]
        public void Armor_collision_call_armor_hit()
        {
            _armorComponent.Init(_armor);
            
            _armorComponent.OnCollisionEnter2D(new Collision2D());
            
            _armor.Received().Hit();
        }
        
        
        [Test]
        public void Armor_collision_doesnt_work_after_deinit()
        {
            _armorComponent.Init(_armor).Dispose();
            
            _armorComponent.OnCollisionEnter2D(new Collision2D());
            
            _armor.DidNotReceive().Hit();
        }

    }
    public class ArmorTest
    {
        private OneHitArmor _armor;
        private GameEntity _entity;
    
        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
            _armor = new OneHitArmor(_entity);
            _entity.Spawn();
        }

        [Test]
        public void Die_called_when_hit()
        {
            bool died = false;
            _entity.Died += () => died = true; 
            
            _armor.Hit();
            
            Assert.IsTrue(died);
        }
    }
}