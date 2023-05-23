using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class PlayerPrefStorage : IStorage<int>
    {
        private readonly string _key;

        public PlayerPrefStorage(string key)
        {
            _key = key;
        }

        public int Load()
        {
            return PlayerPrefs.GetInt(_key, 0);
        }

        public void Save(int value)
        {
            PlayerPrefs.SetInt(_key, value);
        }
    }
}