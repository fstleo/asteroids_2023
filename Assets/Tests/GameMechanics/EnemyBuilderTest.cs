using Asteroids.GameMechanics.Enemies;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics
{
    public class EnemyBuilderTest
    {
        private EnemySpawner _asteroidsBuilder;
        private IFactory<string, EntityBuilder> _entityBuilder;
        private readonly static AsteroidPrefab [] _prefabs = {
            new()
            {
                Type = EnemyType.SmallAsteroid,
                PrefabId = "Small"
            },
            new()
            {
                Type = EnemyType.MediumAsteroid,
                PrefabId = "Medium"
            },
            new()
            {
                Type = EnemyType.LargeAsteroid,
                PrefabId = "Large"
            }
        };

        private EntityBuilder _entity;

        private AsteroidsSettings _settings = new AsteroidsSettings
        {
            MinimumSpeed = 4f,
            MaximumSpeed = 6f,
            MinimumRotationSpeed = 1f,
            MaximumRotationSpeed = 2f,
            Prefabs = _prefabs
        };
        
        [SetUp]
        public void Setup()
        {
            _entity = new EntityBuilder();
            _entityBuilder = Substitute.For<IFactory<string, EntityBuilder>>();
            _entityBuilder.Create(Arg.Any<string>()).Returns(_entity);
            _asteroidsBuilder = new EnemySpawner(_entityBuilder, _settings);
        }

        [Test]
        [TestCaseSource(nameof(_prefabs))]
        public void Create_asteroid_calls_factory_with_prefab_id(AsteroidPrefab prefab)
        {
            var entity = _asteroidsBuilder.Create(prefab.Type);

            _entityBuilder.Received().Create(prefab.PrefabId);
            
            Assert.Less(entity.Velocity.Value.magnitude, _settings.MaximumSpeed);
            Assert.Greater(entity.Velocity.Value.magnitude, _settings.MinimumSpeed);
            Assert.Less(entity.RotationSpeed.Value, _settings.MaximumRotationSpeed);
            Assert.Greater(entity.RotationSpeed.Value, _settings.MinimumRotationSpeed);
        }
        
    }
}
