using UnityEngine;

namespace Asteroids.GameMechanics.Player.PlayerLife
{
    [CreateAssetMenu(menuName = "Settings/Player lives")]
    public class PlayerLifeSettingsSO : ScriptableObject
    {
        [field:SerializeField]
        public PlayerLifeSettings PlayerLifeSettings { get; private set; }
    }
}
