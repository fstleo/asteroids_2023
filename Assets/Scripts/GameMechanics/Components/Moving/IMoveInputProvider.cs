using System;
using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.Moving
{
    public interface IMoveInputProvider : IEntityComponent
    {
        IObservable<bool> Thrust { get; }
        IObservable<RotationInput> TurnInput { get; }
    }
}