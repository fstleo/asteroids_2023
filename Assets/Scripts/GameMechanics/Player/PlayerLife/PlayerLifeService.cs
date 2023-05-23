using System;
using System.Threading.Tasks;
using Asteroids.Game;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.GameMechanics.Player.PlayerLife
{


    /// <summary>
    /// Handles player spawning
    /// </summary>
    public class PlayerLifeService : IPlayerLifeService
    {
        
        // move live counter to the Game class?
        public IObservable<int> LivesCount => _livesCount;
        private readonly ReactiveProperty<int> _livesCount = new();
        

        private readonly GameStateKeeper _game;
        private readonly IAsteroidField _asteroidField;
        private readonly IFactory<GameEntity> _playerFactory;
        private readonly IWaiter _waiter;
        private readonly TimeSpan _respawnTime;

        public PlayerLifeService(GameStateKeeper game, IAsteroidField asteroidField, 
                                 IFactory<GameEntity> playerFactory, IWaiter waiter, IPlayerLifeSettings lifeSettings)
        {
            _game = game;
            _asteroidField = asteroidField;
            _playerFactory = playerFactory;
            _waiter = waiter;
            _respawnTime = TimeSpan.FromSeconds(lifeSettings.RespawnSeconds);
            _livesCount.Value = lifeSettings.LivesCount;
        }

        public void SpawnPlayer()
        {
            var player = _playerFactory.Create();
            player.Position.Value = Vector2.zero;
            player.Died += WaitAndRespawn;
            _asteroidField.AddPlayer(player);

            void WaitAndRespawn()
            {
                _livesCount.Value--;
                player.Died -= WaitAndRespawn;
                if (_livesCount.Value > 0)
                {
                    _ = WaitAndRespawnAsync();    
                }
                else
                {
                    _game.State = GameState.GameOver;
                }
            }
        }

        private async Task WaitAndRespawnAsync()
        {
            await _waiter.Wait((int)_respawnTime.TotalSeconds);
            SpawnPlayer();
        }
    }
}