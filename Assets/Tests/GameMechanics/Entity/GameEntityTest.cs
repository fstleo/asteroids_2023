using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Moving
{
    public class GameEntityTest
    {
        private GameEntity _entity;
        
        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
        }

        [Test]
        public void Tick_listeners_components_called()
        {
            var tickListener = Substitute.For<IEntityComponent, ITickListener>();
            _entity.AddComponent(tickListener);
            
            _entity.Tick(2f);
            
            (tickListener as ITickListener).Received().Tick(2f);
        }
        
        [Test]
        public void Spawn_listeners_components_called()
        {
            var spawnListener = Substitute.For<IEntityComponent, ISpawnListener>();
            _entity.AddComponent(spawnListener);
            
            _entity.Spawn();
            
            (spawnListener as ISpawnListener).Received().Spawn();
        }
        
        [Test]
        public void Death_listeners_components_called()
        {
            var deathListener = Substitute.For<IEntityComponent, IDeathListener>();
            _entity.AddComponent(deathListener);
            _entity.Spawn();
            _entity.Die();
            
            (deathListener as IDeathListener).Received().Die();
        }
        
        [Test]
        public void Get_component_returns_component()
        {
            var armor = Substitute.For<IArmor>();
            _entity.AddComponent(armor);

            var armorFromEntity = _entity.GetComponent<IArmor>();
            
            Assert.AreEqual(armor, armorFromEntity);
        }
        
        [Test]
        public void Get_component_returns_null_if_no_component()
        {
            Assert.IsNull(_entity.GetComponent<IArmor>());
        }
        
        [Test]
        public void Cant_die_twice()
        {
            int diedCalled = 0;
            _entity.Died += () => diedCalled++;
            _entity.Spawn();
            _entity.Die();
            _entity.Die();
            
            Assert.AreEqual(1, diedCalled);
        }
        
        [Test]
        public void Death_event_called()
        {
            bool diedCalled = false;
            _entity.Died += () => diedCalled = true;
            _entity.Spawn();
            _entity.Die();
            
            Assert.IsTrue(diedCalled);
        }
    }
}
