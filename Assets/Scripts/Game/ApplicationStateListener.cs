using Asteroids.Infrastructure;

namespace Asteroids.Game
{

    /// <summary>
    /// Converts game state changes to application events
    /// </summary>
    public class ApplicationStateListener
    {
        private readonly IApplication _application;

        public ApplicationStateListener(GameStateKeeper gameStateKeeper, IApplication application)
        {
            _application = application;
            gameStateKeeper.StateChanged += GameStateChange;
        }

        private void GameStateChange(GameState oldState, GameState newState)
        {
            if (newState == GameState.Exit)
            {
                _application.Quit();
                return;
            }
            
            if (oldState == GameState.GameOver && newState == GameState.Game)
            {
                _application.Reload();
            }

        }
    }
}
