using System;

namespace Asteroids.GameMechanics.Player.PlayerLife
{
    public interface IPlayerLifeService
    {
        IObservable<int> LivesCount { get; }
    }
}
