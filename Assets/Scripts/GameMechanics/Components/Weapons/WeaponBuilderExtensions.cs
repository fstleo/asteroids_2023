using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Components.Weapons
{
    public static class WeaponBuilderExtensions
    {
        public static EntityBuilder WithProjectileWeapon(this EntityBuilder builder, IWeaponInputProvider weaponInputProvider,  float cooldown, IFactory<GameEntity> projectileFactory)
        {
            return builder.WithComponent(entity =>
            {
                var weapon = new ProjectileWeapon(entity, cooldown, projectileFactory);
                weaponInputProvider.Shot.Subscribe(weapon.Shot);
                return weapon;
            });
        }
    }
}