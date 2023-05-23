using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Enemies;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics
{

    public class EnemyCreatorTest
    {
        private IAsteroidField _asteroidField;
        private IFactory<EnemyType, GameEntity> _asteroidsFactory;
        private GameEntity _mediumAsteroid;
        private IEnemyCreator _enemyCreator;
        
        [SetUp]
        public void Setup()
        {
            _asteroidField = Substitute.For<IAsteroidField>();
            _asteroidsFactory = Substitute.For<IFactory<EnemyType, GameEntity>>();
            
            _enemyCreator = new FieldEnemySpawner(_asteroidField, _asteroidsFactory);
        }

        [Test]
        public void Put_on_field_creates_entity()
        {
            var smallAsteroid = new GameEntity();
            _asteroidsFactory.Create(Arg.Any<EnemyType>()).Returns(smallAsteroid);
            _enemyCreator.PutOnField(EnemyType.SmallAsteroid);

            _asteroidsFactory.Received().Create(EnemyType.SmallAsteroid);
            _asteroidField.Received().AddEnemy(smallAsteroid);
        }
        
        [Test]
        public void Medium_asteroid_after_death_spawns_two_small_asteroids()
        {
            var smallAsteroid = new GameEntity();
            _asteroidsFactory.Create(EnemyType.SmallAsteroid).Returns(smallAsteroid);
            var mediumAsteroid = new GameEntity();
            _asteroidsFactory.Create(EnemyType.MediumAsteroid).Returns(mediumAsteroid);
            
            _enemyCreator.PutOnField(EnemyType.MediumAsteroid);
            mediumAsteroid.Spawn();
            _asteroidsFactory.Received().Create(EnemyType.MediumAsteroid);
            _asteroidField.Received().AddEnemy(mediumAsteroid);
            mediumAsteroid.Die();

            _asteroidsFactory.Received(2).Create(EnemyType.SmallAsteroid);
            _asteroidField.Received(2).AddEnemy(smallAsteroid);
        }
        
        [Test]
        public void Large_asteroid_after_death_spawns_two_medium_asteroids()
        {
            var mediumAsteroid = new GameEntity();
            _asteroidsFactory.Create(EnemyType.MediumAsteroid).Returns(mediumAsteroid);
            var largeAsteroid = new GameEntity();
            _asteroidsFactory.Create(EnemyType.LargeAsteroid).Returns(largeAsteroid);
            
            _enemyCreator.PutOnField(EnemyType.LargeAsteroid);
            largeAsteroid.Spawn();
            _asteroidsFactory.Received().Create(EnemyType.LargeAsteroid);
            _asteroidField.Received().AddEnemy(largeAsteroid);
            largeAsteroid.Die();

            _asteroidsFactory.Received(2).Create(EnemyType.MediumAsteroid);
            _asteroidField.Received(2).AddEnemy(mediumAsteroid);
        }

    }
}
