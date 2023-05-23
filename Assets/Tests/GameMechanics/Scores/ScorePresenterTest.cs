using System;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.SelfDestruction;
using Asteroids.GameMechanics.Entities;
using Asteroids.GameMechanics.Player;
using Asteroids.GameMechanics.Scores;
using Asteroids.GameMechanics.Scores.UI;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Scores
{

    public class ProjectileBuilderTest
    {
        private ProjectileBuilder _projectileBuilder;
        private IFactory<string, EntityBuilder> _defaultBuilder;
        private GameEntity _entity;
        private const string PrefabName = "Projectile";

        [SetUp]
        public void Setup()
        {
            _defaultBuilder = Substitute.For<IFactory<string, EntityBuilder>>();
            _defaultBuilder.Create(Arg.Any<string>()).Returns(new EntityBuilder());
            _projectileBuilder = new ProjectileBuilder(_defaultBuilder, PrefabName, 1f, new AccelerationSettings
            {
                Acceleration = 4,
                MaximumSpeed = 4,
                RotationSpeed = 1
            });
            _entity = _projectileBuilder.Create();
        }

        [Test]
        public void Creates_from_default()
        {
            _defaultBuilder.Received().Create(PrefabName);
        }

        [Test]
        public void Will_self_destruct()
        {
            Assert.IsNotNull(_entity.GetComponent<SelfDestruction>());
        }
    }

    public class ScorePresenterTest
    {
        private ScorePresenter _scorePresenter;
        private IScoresWidget _scoresWidget;
        private IScoreService _scoresService;
        private IDisposable _scoresSubscription;
        private IDisposable _topScoresSubscription;
        
        [SetUp]
        public void Setup()
        {
            _scoresSubscription = Substitute.For<IDisposable>();
            _topScoresSubscription = Substitute.For<IDisposable>();
            
            _scoresService = Substitute.For<IScoreService>();
            _scoresService.Score.Returns(Substitute.For<IObservable<int>>());
            _scoresService.TopScore.Returns(Substitute.For<IObservable<int>>());
            
            _scoresService.Score.Subscribe(Arg.Any<IObserver<int>>()).Returns(_scoresSubscription);
            _scoresService.TopScore.Subscribe(Arg.Any<IObserver<int>>()).Returns(_topScoresSubscription);
            
            _scoresWidget = Substitute.For<IScoresWidget>();
            _scorePresenter = new ScorePresenter(_scoresService, _scoresWidget);
        }

        [Test]
        public void Widget_subscribed_to_service()
        {
            _scoresService.Score.Received().Subscribe(Arg.Any<IObserver<int>>());
            _scoresService.TopScore.Received().Subscribe(Arg.Any<IObserver<int>>());
        }

        [Test]
        public void Subscriptions_are_disposed_on_dispose()
        {
            _scorePresenter.Dispose();
            
            _scoresSubscription.Received().Dispose();
            _topScoresSubscription.Received().Dispose();
        }

    }
}
