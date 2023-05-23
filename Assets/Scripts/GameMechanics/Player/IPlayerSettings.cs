using Asteroids.GameMechanics.Components.Moving;

namespace Asteroids.GameMechanics.Player
{
    public interface IPlayerSettings
    {
        float WeaponCooldown { get; }
        AccelerationSettings ProjectileSettings { get; }
        AccelerationSettings PlayerAccelerationsSettings { get; }
        string PrefabId { get; }
    }
}