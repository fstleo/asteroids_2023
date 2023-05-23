using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    public class RandomSprite : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _sprites;

        [SerializeField] 
        private SpriteRenderer _renderer;
        

        private void OnEnable()
        {
            _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        }
    }
}
