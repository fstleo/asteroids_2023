using Asteroids.GameMechanics.Entities;

namespace GameMechanics.Components.WrapPosition
{
    public static class WrapPositionBuilderExtension
    {
        public static EntityBuilder KeepInBounds(this EntityBuilder entityBuilder, Bounds2D bounds)
        {
            return entityBuilder.WithComponent(entity => new WrapPosition(entity, bounds));
        }
    }
}