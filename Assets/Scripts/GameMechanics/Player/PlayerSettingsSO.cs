using Asteroids.GameMechanics.Components.Moving;
using UnityEngine;

namespace Asteroids.GameMechanics.Player
{

    [CreateAssetMenu(menuName = "Settings/Player")]
    public class PlayerSettingsSO : ScriptableObject, IPlayerSettings
    {
        [field: SerializeField]
        public AccelerationSettings ProjectileSettings { get; private set; }
        
        [field:SerializeField]
        public float ProjectileLifetime { get; private set; }
        
        [field:SerializeField]
        public float WeaponCooldown { get; private set; }

        [field:SerializeField]
        public AccelerationSettings PlayerAccelerationsSettings { get; private set; }

        [field:SerializeField]
        public string PrefabId { get; private set; }
    }

}