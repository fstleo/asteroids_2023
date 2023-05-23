using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure.Update;
using UnityEngine;

namespace Asteroids.GameMechanics.Components.Moving
{
  
    /// <summary>
    /// Control entity velocity and rotation 
    /// </summary>
    public class Accelerator : IEntityComponent, ITickListener
    {
        private readonly GameEntity _entity;
        private readonly AccelerationSettings _settings;
        
        private bool _thrust;
        private RotationInput _rotationInput;
        
        public Accelerator(GameEntity entity, AccelerationSettings settings) 
        {
            _entity = entity;
            _settings = settings;
        }

        public void SetThrust(bool thrust)
        {
            _thrust = thrust;
        }

        public void SetRotationInput(RotationInput rotationInput)
        {
            _rotationInput = rotationInput;
        }

        public void Tick(float deltaTime)
        {
            _entity.RotationSpeed.Value = (float)_rotationInput * _settings.RotationSpeed ;

            var direction = _thrust ? CalculateDirection() : -_entity.Velocity.Value.normalized;
            var deltaVelocity = direction * _settings.Acceleration * deltaTime;
            if (!_thrust)
            {
                deltaVelocity = CapVelocityToZero(deltaVelocity, ref deltaTime);
            }
            _entity.Velocity.Value += deltaVelocity;
            if (_thrust)
            {
                CapVelocityToMax();
            }
        }

        
        private Vector2 CalculateDirection()
        {
            var up = Vector2.up;
            var angleRad = Mathf.Deg2Rad * _entity.Angle.Value; 
            return new Vector2(up.x * Mathf.Cos(angleRad) - up.y * Mathf.Sin(angleRad), up.x * Mathf.Sin(angleRad) + up.y * Mathf.Cos(angleRad));
        }

        private Vector2 CapVelocityToZero(Vector2 deltaVelocity, ref float deltaTime)
        {
            if (deltaVelocity.sqrMagnitude < _entity.Velocity.Value.sqrMagnitude)
            {
                return deltaVelocity;
            }
            //only decelerate for time to full stop
            deltaTime *= _entity.Velocity.Value.magnitude / deltaVelocity.magnitude;
            deltaVelocity = -_entity.Velocity.Value;
            return deltaVelocity;
        }

        private void CapVelocityToMax()
        {
            if (_entity.Velocity.Value.sqrMagnitude > _settings.MaximumSpeed * _settings.MaximumSpeed)
            {
                _entity.Velocity.Value = _entity.Velocity.Value.normalized * _settings.MaximumSpeed;
            }
        }
    }
}