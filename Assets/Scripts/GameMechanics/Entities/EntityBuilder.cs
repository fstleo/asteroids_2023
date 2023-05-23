using System;
using System.Collections.Generic;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Entities
{
    
    /// <summary>
    /// Create entity with components and spawn/death actions
    /// </summary>
    public class EntityBuilder : IFactory<GameEntity>
    {
        private readonly List<Action<GameEntity>> _deathActions = new(4);
        private readonly List<Action<GameEntity>> _spawnActions = new(4);
        private readonly List<Func<GameEntity, IEntityComponent>> _componentsFactories = new(4);
        private readonly MemoryPool<GameEntity> _entitiesPool = new();
        
        public EntityBuilder WithComponent(Func<GameEntity, IEntityComponent> componentFactory)
        {
            _componentsFactories.Add(componentFactory);
            return this;
        }
        
        public EntityBuilder OnSpawn(Action<GameEntity> action)
        {
            _spawnActions.Add(action);
            return this;
        }

        public EntityBuilder OnDeath(Action<GameEntity> action)
        {
            _deathActions.Add(action);
            return this;
        }
        
        public GameEntity Create()
        {
            if (!_entitiesPool.TryGet(out var entity))
            {
                entity = new GameEntity();
                foreach (var componentsFactory in _componentsFactories)
                {
                    entity.AddComponent(componentsFactory.Invoke(entity));
                }
            }
            
            foreach (var spawnAction in _spawnActions)
            {
                spawnAction?.Invoke(entity);
            }
            entity.Spawn();
            entity.Died += CallOnDeathActions;

            return entity;

            void CallOnDeathActions()
            {
                entity.Died -= CallOnDeathActions;
                foreach (var deathAction in _deathActions)
                {
                    deathAction?.Invoke(entity);
                }
                _entitiesPool.Return(entity);
            }
        }

    }
}