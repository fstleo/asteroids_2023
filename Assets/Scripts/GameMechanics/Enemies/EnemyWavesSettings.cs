using System;
using UnityEngine;

namespace Asteroids.GameMechanics.Enemies
{
    [Serializable]
    public class EnemyWavesSettings : IEnemyWavesSettings
    {
        public EnemyWave[] Waves;

        public EnemyWave GetWave(int index)
        {
            return Waves[Mathf.Clamp(index, 0, Waves.Length - 1)];
        }
    }

}