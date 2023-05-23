using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using Asteroids.Infrastructure.Update;
using UnityEngine;

namespace Asteroids.GameMechanics.Components.Weapons
{

    public class ProjectileWeapon : IEntityComponent, ITickListener
    {
        private readonly GameEntity _entity;
        private readonly IFactory<GameEntity> _projectileFactory;

        private readonly float _cooldown;
        private float _currentCooldown;
        
        public ProjectileWeapon(GameEntity entity, float cooldown, IFactory<GameEntity> projectileFactory)
        {
            _entity = entity;
            _projectileFactory = projectileFactory;
            _cooldown = cooldown;
        }
        
        public void Shot(bool shot)
        {
            if (!shot || _currentCooldown > Mathf.Epsilon)
            {
                return;
            }
            var projectile = _projectileFactory.Create();
            projectile.Position.Value = _entity.Position.Value;
            projectile.Angle.Value = _entity.Angle.Value;
            projectile.Velocity.Value = Vector2.zero;
            _currentCooldown = _cooldown; 
        }

        public void Tick(float deltaTime)
        {
            _currentCooldown = Mathf.Max(_currentCooldown - deltaTime,0);
        }
    }
}