using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    /// <summary>
    /// Sets rigidbody position 
    /// </summary>
    public class PositionComponent : ObserverBehaviour<Vector2>
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public override void OnNext(Vector2 position)
        {
            _rigidbody.position = position;
        }

    }
}