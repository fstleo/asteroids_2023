using System;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Scores
{

    public class ScoreService : IScoreService
    {
        private readonly IStorage<int> _topScoreStorage;
        
        private readonly ReactiveProperty<int> _score = new();
        public IObservable<int> Score => _score;
        
        private readonly ReactiveProperty<int> _topScore = new();
        public IObservable<int> TopScore => _topScore;


        public ScoreService(IAsteroidField asteroidField, IStorage<int> topScoreStorage)
        {
            _topScoreStorage = topScoreStorage;
            _topScore.Value = _topScoreStorage.Load();
            asteroidField.EnemyDestroyed += IncreaseScore;
        }

        private void IncreaseScore(GameEntity entity)
        {
            _score.Value += 100;
            UpdateIfTopScore(_score.Value);
        }

        private void UpdateIfTopScore(int score)
        {
            if (score <= _topScore.Value)
            {
                return;
            }
            _topScore.Value = score;
            _topScoreStorage.Save(score);
        }

    }
}