using System;
using UnityEngine;

namespace Asteroids.GameMechanics.Player.PlayerLife
{

    [Serializable]
    public class PlayerLifeSettings : IPlayerLifeSettings
    {
        [field:SerializeField]
        public int LivesCount { get; set; }
        
        [field:SerializeField]
        public int RespawnSeconds { get; set; }
        
    }
}
