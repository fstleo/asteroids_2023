using Asteroids.Game;
using Asteroids.Infrastructure.Update;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.EnemiesSpawn
{
    public class VContainerUpdateRunnerTest
    {
        private GameStateKeeper _game;
        private ITickListener _tickListener;
        private VContainerUpdateRunner _updateRunner;
        
        [SetUp]
        public void Setup()
        {
            _game = new GameStateKeeper();
            _tickListener = Substitute.For<ITickListener>();
            _updateRunner = new VContainerUpdateRunner(_game, new []{_tickListener});
        }

        [Test]
        public void Runs_for_created_tick_listeners()
        {
            _updateRunner.Tick();
            
            _tickListener.Received().Tick(Arg.Any<float>());
        }
        
        
        [Test]
        public void Runs_for_added_tick_listeners()
        {
            var dynamicTickListener = Substitute.For<ITickListener>();
            _updateRunner.AddListener(dynamicTickListener);
            _updateRunner.Tick();
            
            dynamicTickListener.Received().Tick(Arg.Any<float>());
        }
        
        [Test]
        public void Doesnt_run_after_removing_tick_listener()
        {
            var dynamicTickListener = Substitute.For<ITickListener>();
            _updateRunner.AddListener(dynamicTickListener);
            _updateRunner.RemoveListener(dynamicTickListener);
            _updateRunner.Tick();

            dynamicTickListener.DidNotReceive().Tick(Arg.Any<float>());
        }
        
        [Test]
        public void Doesnt_run_if_game_is_paused()
        {
            _game.State = GameState.Pause;
            
            _updateRunner.Tick();
            
            _tickListener.DidNotReceive().Tick(Arg.Any<float>());
        }

    }
}
