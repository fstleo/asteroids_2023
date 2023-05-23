using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.Armor
{
    public static class ArmorBuilderExtensions
    {
        public static EntityBuilder WithOneHitArmor(this EntityBuilder builder)
        {
            return builder.WithComponent(entity => new OneHitArmor(entity));
        }
    }
}