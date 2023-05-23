using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.SelfDestruction;
using Asteroids.GameMechanics.Entities;
using Asteroids.GameMechanics.Views.Entity;
using Asteroids.Infrastructure;
using Asteroids.Infrastructure.Update;
using GameMechanics.Components.WrapPosition;
using UnityEngine;

namespace Asteroids.GameMechanics
{
    
    /// <summary>
    /// Build entity with common components
    /// </summary>
    public class DefaultEntityBuilder : IFactory<string, EntityBuilder>
    {
        private readonly IAsteroidField _asteroidField;
        private readonly ITickSource _updateRunner;
        private readonly IProvider<GameObject> _viewFactory;

        public DefaultEntityBuilder(IAsteroidField asteroidField, ITickSource updateRunner, IProvider<GameObject> viewFactory)
        {
            _asteroidField = asteroidField;
            _updateRunner = updateRunner;
            _viewFactory = viewFactory;

        }
        
        public EntityBuilder Create(string prefabId)
        {
            var deathEffectView = new EntityBuilder()
                                  .SelfDestructIn(0.5f)
                                  .WithView(_viewFactory, prefabId + "_Death");
            return new EntityBuilder()
                   .WithSimpleEngine()
                   .WithOneHitArmor()
                   .KeepInBounds(_asteroidField.Bounds)
                   .ListenToUpdate(_updateRunner)
                   .WithView(_viewFactory, prefabId)
                   .OnDeath(entity =>
                   {
                       var deathEffect = deathEffectView.Create();
                       deathEffect.Position.Value = entity.Position.Value;
                   });

        }
    }
}
