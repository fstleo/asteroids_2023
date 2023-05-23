using Asteroids.Game;
using Asteroids.GameMechanics.Components.Input;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.GameStateChange
{
    public class PauseListenerTest
    {
        private PauseListener _pauseListener;
        private GameStateKeeper _game;
        private IKeyboardInput _input;
        
        [SetUp]
        public void Setup()
        {
            _game = new GameStateKeeper();
            _input = Substitute.For<IKeyboardInput>();
            _pauseListener = new PauseListener(_game, _input);
        }
        
        [Test]
        public void Game_state_stays_if_esc_pressed()
        {
            _game.State = GameState.Game;
            _input.GetKeyDown(KeyCode.Escape).Returns(false);
            
            _pauseListener.Tick();
            
            Assert.AreEqual(GameState.Game, _game.State);
        }


        [Test]
        public void Game_state_changes_from_game_to_pause_if_esc_pressed()
        {
            _game.State = GameState.Game;
            _input.GetKeyDown(KeyCode.Escape).Returns(true);
            
            _pauseListener.Tick();
            
            Assert.AreEqual(GameState.Pause, _game.State);
        }
        
        [Test]
        public void Game_state_changes_from_pause_to_game_if_esc_pressed()
        {
            _game.State = GameState.Pause;
            _input.GetKeyDown(KeyCode.Escape).Returns(true);
            
            _pauseListener.Tick();
            
            Assert.AreEqual(GameState.Game, _game.State);
        }
    }
}
