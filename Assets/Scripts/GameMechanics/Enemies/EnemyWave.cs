using System;

namespace Asteroids.GameMechanics.Enemies
{
    [Serializable]
    public class EnemyWave
    {
        [Serializable]
        public class Enemy
        {
            public EnemyType Type;
            public ushort Count;
        }

        public Enemy[] Enemies;
    }
}