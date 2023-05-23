using UnityEngine;

namespace Asteroids.GameMechanics.Views
{
    /// <summary>
    /// Shows sprite if state is on or off if inversed
    /// </summary>
    public class EnableSprite : ObserverBehaviour<bool>
    {
        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private bool _inversed;
        
        public override void OnNext(bool data)
        {
            if (_renderer != null)
            {
                _renderer.enabled = data ^ _inversed;
            }
        }

    }
}