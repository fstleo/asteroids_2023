using System;

namespace Asteroids.Game.GameMenu
{
    
    /// <summary>
    /// Pause or game over menu
    /// </summary>
    public class GameMenuPresenter : IDisposable
    {
        private readonly IGameMenu _menu;
        private readonly GameStateKeeper _gameStateKeeper;

        public GameMenuPresenter(IGameMenu menu, GameStateKeeper gameStateKeeper)
        {
            _menu = menu;
            _gameStateKeeper = gameStateKeeper;
            _menu.Exit += OnExit;
            _menu.BackToTheGame += OnBackToTheGame;
        }
        
        private void OnExit()
        {
            _gameStateKeeper.State = GameState.Exit;
        }

        private void OnBackToTheGame()
        {
            _gameStateKeeper.State = GameState.Game;
        }

        public void Dispose()
        {
            _menu.Exit -= OnExit;
            _menu.BackToTheGame -= OnBackToTheGame;
        }
    }
}