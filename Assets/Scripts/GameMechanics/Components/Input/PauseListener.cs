using Asteroids.Game;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.GameMechanics.Components.Input
{
    public class PauseListener : ITickable
    {
        private readonly GameStateKeeper _game;
        private readonly IKeyboardInput _playerInput;

        public PauseListener(GameStateKeeper game, IKeyboardInput playerInput)
        {
            _game = game;
            _playerInput = playerInput;
        }
        
        public void Tick()
        {
            if (!_playerInput.GetKeyDown(KeyCode.Escape))
            {
                return;
            }
            switch (_game.State)
            {
                case GameState.Game:
                    _game.State = GameState.Pause;
                    return;
                case GameState.Pause:
                    _game.State = GameState.Game;
                    return;
            }
        }
    }
}
