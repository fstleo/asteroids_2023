using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;

namespace Asteroids.GameMechanics.Components.SelfDestruction
{

    public class SelfDestruction : IEntityComponent, ITickListener, ISpawnListener
    {
        private readonly GameEntity _entity;
        private readonly float _lifeTime;
        private float _lifeTimeLeft;

        public SelfDestruction(GameEntity entity, float lifeTime)
        {
            _entity = entity;
            _lifeTime = lifeTime;
        }
        
        public void Tick(float deltaTime)
        {
            _lifeTimeLeft -= deltaTime;
            if (_lifeTimeLeft < 0)
            {
                _entity.Die();
            }
        }

        public void Spawn()
        {
            _lifeTimeLeft = _lifeTime;
        }
    }
}
