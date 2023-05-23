using System;
using Asteroids.Game;
using Asteroids.Game.GameMenu;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.GameStateChange
{
    public class GameButtonsPresenterTest
    {
        private GameStateKeeper _game;
        private GameMenuPresenter _gameMenuPresenter;
        private IGameMenu _gameMenu;
        
        [SetUp]
        public void Setup()
        {
            _gameMenu = Substitute.For<IGameMenu>();
            _game = new GameStateKeeper();
            _game.State = GameState.Pause;
            _gameMenuPresenter = new GameMenuPresenter(_gameMenu, _game);
        }

        [Test]
        public void Menu_exit_click_change_game_state_to_exit()
        {
            _gameMenu.Exit += Raise.Event<Action>();
            
            Assert.AreEqual(GameState.Exit, _game.State);
        }
        
        [Test]
        public void Menu_back_to_the_game_click_change_game_state_to_game()
        {
            _gameMenu.BackToTheGame += Raise.Event<Action>();
            
            Assert.AreEqual(GameState.Game, _game.State);
        }
        
        [Test]
        public void Menu_back_to_the_game_click_doesnt_change_state_after_dispose()
        {
            _gameMenuPresenter.Dispose();
            
            _gameMenu.BackToTheGame += Raise.Event<Action>();
            
            Assert.AreNotEqual(GameState.Game, _game.State);
        }
        
        [Test]
        public void Menu_exit_click_doesnt_change_state_after_dispose()
        {
            _gameMenuPresenter.Dispose();
            
            _gameMenu.Exit += Raise.Event<Action>();
            
            Assert.AreNotEqual(GameState.Exit, _game.State);
        }
    }
}
