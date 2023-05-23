using Asteroids.GameMechanics.Entities;

namespace Asteroids.GameMechanics.Components.Armor
{

    public interface IArmor : IEntityComponent
    {
        void Hit();
    }
}