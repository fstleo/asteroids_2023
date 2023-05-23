using System;
using System.Collections.Generic;
using Asteroids.GameMechanics.Entities;
using UnityEngine;

namespace Asteroids.GameMechanics.AsteroidField
{
    public interface IAsteroidField
    {

        public Bounds2D Bounds { get; }
        public Vector2 GetRandomPoint();

        public GameEntity Player { get; }
        event Action<GameEntity> PlayerCreated;
        event Action<GameEntity> PlayerDestroyed;
        void AddPlayer(GameEntity player);

        event Action<GameEntity> EnemyCreated;
        event Action<GameEntity> EnemyDestroyed;
        IReadOnlyList<GameEntity> Enemies { get; }

        void AddEnemy(GameEntity asteroid);
        void RemoveEnemy(GameEntity asteroid);
    }
}