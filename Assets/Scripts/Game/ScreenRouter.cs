using System;
using System.Linq;
using Asteroids.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    /// <summary>
    /// Switches game screens based on the game state
    /// </summary>
    public class ScreenRouter : LifetimeScope
    {
        // TODO: Replace scope with addressable
        [Serializable]
        private class Screen
        {
            public GameState State;
            public LifetimeScope Scope;
        }

        private LifetimeScope _currentScreenContext;

        [SerializeField]
        private Screen[] _screensContexts;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(resolver =>
            {
                var game = resolver.Resolve<GameStateKeeper>();
                game.StateChanged += SetState;
                SetState(GameState.Exit, game.State);
            });
        }
        
        private void SetState(GameState oldState, GameState newState)
        {
            if (_currentScreenContext != null)
            {
                _currentScreenContext.Dispose();    
                Destroy(_currentScreenContext.gameObject);
            }
            var screen = _screensContexts.FirstOrDefault(s => s.State == newState);
            if (screen != null)
            {
                _currentScreenContext = Instantiate(screen.Scope);
            }
        }
    }
}