using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.Moving
{
    public static class EngineExtensions
    {
        /// <summary>
        /// Adds engine component to move and rotate entity
        /// </summary>
        public static EntityBuilder WithSimpleEngine(this EntityBuilder entityBuilder)
        {
            return entityBuilder.WithComponent(entity => new Engine(entity));
        }
        
        
        /// <summary>
        /// Add accelerator with controls
        /// </summary>
        public static EntityBuilder AddControls(this EntityBuilder entityBuilder, IMoveInputProvider inputProvider, AccelerationSettings settings)
        {
            IMoveInputProvider moveInputProvider = inputProvider;
            return entityBuilder
                .WithComponent(_ => moveInputProvider)
                .WithComponent(entity =>
                {
                    var velocityControls = new Accelerator(entity, settings);
                    moveInputProvider.Thrust.Subscribe(velocityControls.SetThrust);
                    moveInputProvider.TurnInput.Subscribe(velocityControls.SetRotationInput);
                    return velocityControls;
                });
        }
    }
}