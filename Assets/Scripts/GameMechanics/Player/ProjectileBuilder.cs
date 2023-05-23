using Asteroids.GameMechanics.Components.Input;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.SelfDestruction;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Player
{
    /// <summary>
    /// Builds projetile entity
    /// </summary>
    public class ProjectileBuilder : IFactory<GameEntity>
    {
        private readonly EntityBuilder _projectileBuilder;
        
        public ProjectileBuilder(IFactory<string, EntityBuilder> defaultEntity, string prefabName, float lifeTime, AccelerationSettings projectileSettings)
        {
            _projectileBuilder = defaultEntity.Create(prefabName)
                                              .AddControls(new GoForwardInput(), projectileSettings)
                                              .SelfDestructIn(lifeTime);
        }

        public GameEntity Create()
        {
            return _projectileBuilder.Create();
        }
    }
}
