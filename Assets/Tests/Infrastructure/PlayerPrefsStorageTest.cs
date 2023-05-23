using Asteroids.Infrastructure;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Shooting
{
    public class PlayerPrefsStorageTest
    {
        private const string Key = "Test_prefs";

        [Test]
        public void Load_from_prefs()
        {
            var storage = new PlayerPrefStorage(Key);
            
            PlayerPrefs.SetInt(Key, 500);

            int result = storage.Load();
            
            Assert.AreEqual(500, result);
        }
        
        [Test]
        public void Saves_from_prefs()
        {
            var storage = new PlayerPrefStorage(Key);
            
            storage.Save(500);
            
            int result = PlayerPrefs.GetInt(Key);

            Assert.AreEqual(500, result);
        }
    }
}
