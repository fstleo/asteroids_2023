using System;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using Asteroids.GameMechanics.Scores;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Scores
{

    public class ScoreServiceTest
    {
        private GameEntity _fakeEnemy;
        private IAsteroidField _asteroidField;
        private ScoreService _scoreService;
        private IStorage<int> _topScoreStorage;

        [SetUp]
        public void Setup()
        {
            _fakeEnemy = new GameEntity();
            _topScoreStorage = Substitute.For<IStorage<int>>();
            _asteroidField = Substitute.For<IAsteroidField>();
        }
        
        [Test]
        public void Score_increased_after_enemy_death()
        {
            _scoreService = new ScoreService(_asteroidField, _topScoreStorage);
            int scoreUpdatedCall = 0;
            _scoreService.Score.Subscribe(score => scoreUpdatedCall = score);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);

            Assert.AreEqual(100, scoreUpdatedCall);
        }
        
        [Test]
        public void Top_score_loaded_from_player_prefs()
        {
            _topScoreStorage.Load().Returns(200);
            _scoreService = new ScoreService(_asteroidField, _topScoreStorage);

            int topScore = 0;
            _scoreService.TopScore.Subscribe(score => topScore = score);
            _topScoreStorage.Received().Load();
            Assert.AreEqual(200, topScore);
        }        

        [Test]
        public void Top_score_saved_to_player_prefs()
        {
            _scoreService = new ScoreService(_asteroidField, _topScoreStorage);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            
            _topScoreStorage.Received().Save(400);
        }
        
        [Test]
        public void Top_score_not_updated_if_less()
        {
            _topScoreStorage.Load().Returns(800);
            _scoreService = new ScoreService(_asteroidField, _topScoreStorage);
            
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            
            _topScoreStorage.DidNotReceive().Save(400);
        }
        
        [Test]
        public void Top_score_changes_if_new_score_is_bigger()
        {
            _scoreService = new ScoreService(_asteroidField, _topScoreStorage);
            int scoreUpdatedCall = 0;
            _scoreService.TopScore.Subscribe(score => scoreUpdatedCall = score);
            
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            
            Assert.AreEqual(100, scoreUpdatedCall);
            
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            _asteroidField.EnemyDestroyed += Raise.Event<Action<GameEntity>>(_fakeEnemy);
            
            Assert.AreEqual(300, scoreUpdatedCall);
        }
    }

}
