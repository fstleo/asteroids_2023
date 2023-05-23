using System;

namespace Asteroids.GameMechanics.Components.Weapons
{
    public interface IWeaponInputProvider
    {
        IObservable<bool> Shot { get; }
    }
}
