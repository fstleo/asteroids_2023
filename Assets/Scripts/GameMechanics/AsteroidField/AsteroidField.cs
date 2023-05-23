using System;
using System.Collections.Generic;
using Asteroids.GameMechanics.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.GameMechanics.AsteroidField
{
    
    /// <summary>
    /// Tracks entities on the field
    /// </summary>
    public class AsteroidField : IAsteroidField
    {
        public GameEntity Player { get; private set; }
        public event Action<GameEntity> PlayerCreated;
        public event Action<GameEntity> PlayerDestroyed;
        public void AddPlayer(GameEntity player)
        {
            Player = player;
            PlayerCreated?.Invoke(player);
        }

        //move bounds somewhere else?
        public Bounds2D Bounds { get; }
        public Vector2 GetRandomPoint()
        {
            return new Vector2(Random.Range(Bounds.min.x, Bounds.max.x), 
                               Random.Range(Bounds.min.y, Bounds.max.y));
        }

        public AsteroidField(Bounds2D bounds)
        {
            Bounds = bounds;
        }
        
        public event Action<GameEntity> EnemyCreated;

        public event Action<GameEntity> EnemyDestroyed;

        public IReadOnlyList<GameEntity> Enemies => _enemies;

        private readonly List<GameEntity> _enemies = new();
        

        public void AddEnemy(GameEntity asteroid)
        {
            _enemies.Add(asteroid);
            EnemyCreated?.Invoke(asteroid);
        }

        public void RemoveEnemy(GameEntity asteroid)
        {
            _enemies.Remove(asteroid);
            EnemyDestroyed?.Invoke(asteroid);
        }

    }
}