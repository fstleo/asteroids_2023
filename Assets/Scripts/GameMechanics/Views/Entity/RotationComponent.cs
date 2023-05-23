using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    public class RotationComponent : ObserverBehaviour<float>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public override void OnNext(float angle)
        {
            _rigidbody.rotation = angle;
        }
    }
}