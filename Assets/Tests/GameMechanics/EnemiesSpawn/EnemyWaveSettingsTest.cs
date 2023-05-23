using Asteroids.GameMechanics.Enemies;
using NUnit.Framework;

namespace Tests.GameMechanics.EnemiesSpawn
{
    public class EnemyWaveSettingsTest
    {
        private EnemyWave _first;
        private EnemyWave _second;
        private EnemyWave _third;
        private EnemyWavesSettings _settings;
        
        [SetUp]
        public void Setup()
        {
            _first = new EnemyWave();
            _second = new EnemyWave();
            _third = new EnemyWave();
            _settings = new EnemyWavesSettings
            {
                Waves = new[]
                {
                    _first,
                    _second, 
                    _third
                }
            };
        }
        
        [Test]
        public void Setting_returns_from_array()
        {
            Assert.AreEqual(_first, _settings.GetWave(0));
            Assert.AreEqual(_second, _settings.GetWave(1));
            Assert.AreEqual(_third, _settings.GetWave(2));
        }
        
        [Test]
        public void Get_the_last_wave_after_run_out_of_waves()
        {
            Assert.AreEqual(_third, _settings.GetWave(4));
            Assert.AreEqual(_third, _settings.GetWave(8));
        }

    }
}
