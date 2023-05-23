using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.SelfDestruction
{
    public static class SelfDestructionExtension
    {
        public static EntityBuilder SelfDestructIn(this EntityBuilder builder, float seconds)
        {
            return builder.WithComponent(entity => new SelfDestruction(entity, seconds));
        }
    }
}
