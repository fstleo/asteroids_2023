using System;
using System.Collections.Generic;
using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Entities;
using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    public class GameEntityView : ViewBehaviour<GameEntity>
    {
        [SerializeField] private RotationComponent _rotation;
        [SerializeField] private PositionComponent _position;
        [SerializeField] private ArmorComponent _armor;

        protected override IEnumerable<IDisposable> GetSubscriptions(GameEntity data)
        {
            yield return data.Position.Subscribe(_position);
            yield return data.Angle.Subscribe(_rotation);
            yield return _armor.Init(data.GetComponent<IArmor>());
        }
    }
}