using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;

namespace GameMechanics.Components.WrapPosition
{
    public class WrapPosition : IEntityComponent, ITickListener
    {
        private readonly GameEntity _entity;
        private readonly Bounds2D _bounds;

        public WrapPosition(GameEntity entity, Bounds2D bounds)
        {
            _entity = entity;
            _bounds = bounds;
        }

        public void Tick(float deltaTime)
        {
            _entity.Position.Value = _bounds.Warp(_entity.Position.Value);
        }
    }
}
