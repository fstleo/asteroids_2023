using System;

namespace Asteroids.GameMechanics.Scores
{
    public interface IScoreService
    {
        IObservable<int> Score { get; }
        IObservable<int> TopScore { get; }
    }
}