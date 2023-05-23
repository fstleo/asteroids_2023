using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{
    [CreateAssetMenu(menuName = "Settings/Entity Settings")]
    public class EntitySettingsSO : ScriptableObject
    {
        [field:SerializeField]
        public AsteroidsSettingSO AsteroidsSetting { get; set; }

        [field:SerializeField]
        public string PrefabId { get; set; }
    }
}