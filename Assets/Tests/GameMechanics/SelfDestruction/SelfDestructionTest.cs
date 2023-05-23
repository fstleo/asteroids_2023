using Asteroids.GameMechanics.Components.SelfDestruction;
using Asteroids.GameMechanics.Entities;
using NUnit.Framework;

namespace Tests.GameMechanics.Armor
{
    public class SelfDestructionTest
    {
        private SelfDestruction _selfDestructionComponent;
        private GameEntity _entity;
        private bool _entityDied = false;

        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
            _entity.Died += () => _entityDied = true;
            
            _selfDestructionComponent = new SelfDestruction(_entity, 2f);
            _entity.AddComponent(_selfDestructionComponent);
            _entity.Spawn();
            
        }
        
        [TearDown]
        public void TearDown()
        {
            _entityDied = false;
        }

        [Test]
        public void Destroy_after_time_passed()
        {
            _selfDestructionComponent.Tick(3f);
            
            Assert.IsTrue(_entityDied);
        }
        
        [Test]
        public void Timer_reset_on_spawn()
        {
            _selfDestructionComponent.Tick(3f);
         
            _entityDied = false;
            _entity.Spawn();
            
            _selfDestructionComponent.Tick(3f);
            Assert.IsTrue(_entityDied);
        }
        
        
        [Test]
        public void Dont_destroy_if_time_hasnt_passed()
        {
            _entity.Tick(1.5f);
            
            Assert.IsFalse(_entityDied);
        }

    }
}
