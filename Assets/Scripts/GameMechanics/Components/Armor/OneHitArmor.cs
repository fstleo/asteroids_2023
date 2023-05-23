using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.Armor
{
    
    /// <summary>
    /// Doesn't protect, just makes the owner dead 
    /// </summary>
    public class OneHitArmor : IArmor
    {
        private readonly GameEntity _entity;

        public OneHitArmor(GameEntity entity)
        {
            _entity = entity;

        }
        public void Hit()
        {
            _entity.Die();
        }

    }

}