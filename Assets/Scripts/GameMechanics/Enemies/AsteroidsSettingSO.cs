using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{

    [CreateAssetMenu(menuName = "Settings/Asteroids")]
    public class AsteroidsSettingSO : ScriptableObject
    {
        [SerializeField] private AsteroidsSettings _settings;
        public IAsteroidsSettings Settings => _settings;
    }
}