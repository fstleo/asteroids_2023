using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Entities;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Moving
{
    public class EngineExtensionTest
    {
        private EntityBuilder _entityBuilder;

        [SetUp]
        public void Setup()
        {
            _entityBuilder = new EntityBuilder();
        }
        
        [Test]
        public void Engine_added()
        {
            var entity = _entityBuilder.WithSimpleEngine().Create();
            
            Assert.IsNotNull(entity.GetComponent<IEngine>());
        }

        [Test]
        public void Controls_added()
        {
            var movementInput = Substitute.For<IMoveInputProvider>();
            var entity = _entityBuilder.AddControls(movementInput, new AccelerationSettings
            {
                Acceleration = 1f,
                MaximumSpeed = 2f,
                RotationSpeed = 1f
            }).Create();

            Assert.AreEqual(movementInput ,entity.GetComponent<IMoveInputProvider>());
            Assert.IsNotNull(entity.GetComponent<Accelerator>());
        }
    }
}
