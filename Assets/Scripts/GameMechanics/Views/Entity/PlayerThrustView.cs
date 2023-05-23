using System;
using System.Collections.Generic;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Entities;
using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    public class PlayerThrustView : ViewBehaviour<GameEntity>
    {
        
        [SerializeField] private EnableSprite _acceleratingSprite;
        [SerializeField] private EnableSprite _normalSprite;

        protected override IEnumerable<IDisposable> GetSubscriptions(GameEntity entity)
        {
            var engine = entity.GetComponent<IMoveInputProvider>();
            yield return engine.Thrust.Subscribe(_normalSprite);
            yield return engine.Thrust.Subscribe(_acceleratingSprite);
            
        }
    }
}