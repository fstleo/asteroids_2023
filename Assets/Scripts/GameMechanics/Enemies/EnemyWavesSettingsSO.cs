using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{
    [CreateAssetMenu(menuName = "Settings/Enemies waves")]
    public class EnemyWavesSettingsSO : ScriptableObject
    {
        [field:SerializeField] public EnemyWavesSettings Waves { get; private set; }

    }
}