using System;
using Asteroids.GameMechanics.Components.Moving;

namespace Asteroids.GameMechanics.Components.Input
{
    public class GoForwardInput : IMoveInputProvider
    {
        public IObservable<bool> Thrust { get; } = new ReactiveProperty<bool>(true);
        public IObservable<RotationInput> TurnInput { get; } = new ReactiveProperty<RotationInput>(RotationInput.None);
    }
}