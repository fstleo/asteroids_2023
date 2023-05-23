using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;

namespace Asteroids.GameMechanics.Components.Moving
{
    /// <summary>
    /// Engine to move entity with constant speed in one particular direction 
    /// </summary>
    public class Engine : IEngine, ITickListener
    {
        private readonly GameEntity _entity;
        
        public Engine(GameEntity entity)
        {
            _entity = entity;
        }
        
        public virtual void Tick(float deltaTime)
        {
            UpdateAngle(deltaTime);
            UpdatePosition(deltaTime);
        }

        private void UpdatePosition(float deltaTime)
        {
            _entity.Position.Value += _entity.Velocity.Value * deltaTime;
        }

        private void UpdateAngle(float deltaTime)
        {
            _entity.Angle.Value += _entity.RotationSpeed.Value * deltaTime;
        }
    }
}