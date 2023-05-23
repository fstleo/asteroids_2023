using Asteroids.Game;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.EnemiesSpawn
{
    public class ApplicationStateListenerTest
    {
        private IApplication _application;
        private GameStateKeeper _game;
        
        [SetUp]
        public void Setup()
        {
            _game = new GameStateKeeper();
            _application = Substitute.For<IApplication>();
            new ApplicationStateListener(_game, _application);
        }
        
        [Test]
        public void On_quit_application_quit_called()
        {
            _game.State = GameState.Exit;
            
            _application.Received().Quit();
        }
    
        [Test]
        public void On_switching_from_gameover_to_game_restart_called()
        {
            _game.State = GameState.GameOver;
            _game.State = GameState.Game;
            
            _application.Received().Reload();
        }
    }
}
