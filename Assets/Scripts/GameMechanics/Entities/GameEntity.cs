using System;
using System.Collections.Generic;
using Asteroids.Infrastructure.Update;
using UnityEngine;

namespace Asteroids.GameMechanics.Entities
{
    /// <summary>
    /// Entity represent object on the field
    /// </summary>
    public class GameEntity : ITickListener
    {
        public event Action Died;
        
        public ReactiveProperty<float> RotationSpeed { get; } = new();
        public ReactiveProperty<Vector2> Velocity { get; } = new();
        public ReactiveProperty<Vector2> Position { get; } = new();
        public ReactiveProperty<float> Angle { get; }= new();


        private bool _alive;
        private readonly List<IEntityComponent> _components = new(16);
        
        public void AddComponent(IEntityComponent entityComponent)
        {
            _components.Add(entityComponent);
        }

        public T GetComponent<T>() where T : class
        {
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                {
                    return typedComponent;
                }
            }
            Debug.LogWarning($"Component {typeof(T).Name} not found");
            return null;
        }

        private IEnumerable<T> GetComponents<T>()
        {
            foreach (var component in _components)
            {
                if (component is T result)
                {
                    yield return result;
                }
            }
        }

        public void Spawn()
        {
            foreach (var spawnListener in GetComponents<ISpawnListener>())
            {
                spawnListener.Spawn();    
            }
            _alive = true;
        }

        public void Die()
        {
            if (!_alive)
            {
                return;
            }
            foreach (var deathListener in GetComponents<IDeathListener>())
            {
                deathListener.Die();    
            }
            _alive = false;
            Died?.Invoke();
        }
        
        public void Tick(float deltaTime)
        {
            foreach (var tickListener in GetComponents<ITickListener>())
            {
                tickListener.Tick(deltaTime);
            }
        }
    }
}