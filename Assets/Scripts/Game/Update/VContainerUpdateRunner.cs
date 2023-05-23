using System.Collections.Generic;
using Asteroids.Game;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Infrastructure.Update
{
    public class VContainerUpdateRunner : ITickable, ITickSource
    {
        private readonly GameStateKeeper _gameStateKeeper;
        private readonly List<ITickListener> _listeners;

        public VContainerUpdateRunner(GameStateKeeper gameStateKeeper, IEnumerable<ITickListener> listeners)
        {
            _gameStateKeeper = gameStateKeeper;
            _listeners = new List<ITickListener>(listeners);
        }

        public void Tick()
        {
            if (_gameStateKeeper.State == GameState.Pause)
            {
                return;
            }
            for (var index = 0; index < _listeners.Count; index++)
            {
                _listeners[index].Tick(Time.deltaTime);
            }

        }

        public void AddListener(ITickListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(ITickListener listener)
        {
            _listeners.Remove(listener);
        }
    }

}