using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;

namespace Asteroids.GameMechanics
{
    public static class TickExtensions
    {
        public static EntityBuilder ListenToUpdate(this EntityBuilder builder, ITickSource updateRunner)
        {
            return builder
                   .OnSpawn(updateRunner.AddListener)
                   .OnDeath(updateRunner.RemoveListener);
        }
    }
}