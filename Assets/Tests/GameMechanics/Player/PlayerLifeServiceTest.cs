using System.Threading.Tasks;
using Asteroids.Game;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Entities;
using Asteroids.GameMechanics.Player.PlayerLife;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.PlayerTests
{
    public class PlayerLifeServiceTest
    {

        private PlayerLifeService _playerLifeService;
        private GameEntity _playerEntity;
        private IFactory<GameEntity> _playerFactory;
        private IWaiter _waiter;
        private GameStateKeeper _game;
        
        [SetUp]
        public void Setup()
        {
            _game = new GameStateKeeper();
            _waiter = Substitute.For<IWaiter>();
            _waiter.Wait(Arg.Any<int>()).Returns(Task.CompletedTask);
            _playerEntity = new GameEntity();
            _playerFactory = Substitute.For<IFactory<GameEntity>>();
            
            _playerFactory.Create().Returns(_playerEntity);
            _playerLifeService = new PlayerLifeService(_game, new AsteroidField(new Bounds2D(Vector2.zero, new Vector2(12,12))), _playerFactory, _waiter, 
                new PlayerLifeSettings
                {
                    LivesCount = 5,
                    RespawnSeconds = 1
                });
        }
        
                
        [Test]
        public void Maximum_lives_from_settings()
        {
            int livesCount = 0;
            _playerLifeService.LivesCount.Subscribe(lives => livesCount = lives);
            Assert.AreEqual(5, livesCount);
        }
        
        [Test]
        public void Player_respawns_if_lives_amount_more_than_0()
        {
            _playerLifeService.SpawnPlayer();
            _playerEntity.Spawn();
            _playerEntity.Die();

            _playerFactory.Received(2).Create();
        }

             
        [Test]
        public void Player_doesnt_respawn_if_lives_amount_more_than_0()
        {
            _playerLifeService = new PlayerLifeService(_game, new AsteroidField(new Bounds2D(Vector2.zero, new Vector2(12,12))),_playerFactory,_waiter, new PlayerLifeSettings
            {
                LivesCount = 1,
                RespawnSeconds = 1
            });
            
            _playerLifeService.SpawnPlayer();
            _playerEntity.Spawn();
            _playerEntity.Die();

            _playerFactory.Received(1).Create();
            Assert.AreEqual(GameState.GameOver, _game.State);
        }


        [Test]
        public void Death_reduce_lives_count()
        {
            int livesCount = 0;
            _playerLifeService.LivesCount.Subscribe(lives => livesCount = lives);
            
            _playerLifeService.SpawnPlayer();
            _playerEntity.Spawn();
            _playerEntity.Die();
            
            Assert.AreEqual(4, livesCount);
        }

    }
}