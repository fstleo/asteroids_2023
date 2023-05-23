using System;

namespace Asteroids.Game.GameMenu
{
    public interface IGameMenu
    {
        event Action BackToTheGame;
        event Action Exit;
    }
}