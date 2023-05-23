using Asteroids.Game;
using NUnit.Framework;

namespace Tests.GameMechanics.GameStateChange
{

    public class GameStatesChangeTest
    {
        private GameStateKeeper _game;

        [SetUp]
        public void Setup()
        {
            _game = new GameStateKeeper();
        }

        [Test]
        public void Set_state_event_called()
        {
            var oldStateFromEvent = GameState.Pause;
            var newStateFromEvent = GameState.Pause;
            _game.State = GameState.GameOver;
            _game.StateChanged += (oldState, newState) =>
            {
                oldStateFromEvent = oldState;
                newStateFromEvent = newState;
            };
            _game.State = GameState.Game;
            
            Assert.AreEqual(GameState.GameOver, oldStateFromEvent);
            Assert.AreEqual(GameState.Game, newStateFromEvent);
        }

        
        [Test]
        public void Set_state_changes_game_state()
        {
            _game.State = GameState.Pause;
            
            Assert.AreEqual(GameState.Pause, _game.State);
        }
    }
}
