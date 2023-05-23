using System;

namespace Asteroids.Game
{
    
    /// <summary>
    /// Handles state changing
    /// </summary>
    public class GameStateKeeper
    {
        private GameState _state;
        public event Action<GameState, GameState> StateChanged;

        public GameState State
        {
            get => _state;
            set
            {
                var oldState = _state;
                _state = value;
                StateChanged?.Invoke(oldState,_state);
            }
        }

    }
}