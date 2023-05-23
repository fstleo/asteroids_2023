using Asteroids.GameMechanics;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Armor
{
    
    public class TickExtensionsTest
    {
        
        private ITickSource _tickSource;
        private GameEntity _entity;
        
        [SetUp]
        public void Setup()
        {
            _tickSource = Substitute.For<ITickSource>();
            _entity = new EntityBuilder().ListenToUpdate(_tickSource).Create();
        }

        [Test]
        public void Subscribes_to_tick_on_spawn()
        {
            _entity.Spawn();
            _tickSource.Received().AddListener(_entity);
        }
        
        [Test]
        public void Unsubscribes_from_tick_on_death()
        {
            _entity.Spawn();
            _entity.Die();
            _tickSource.Received().RemoveListener(_entity);
        }
    }
}
