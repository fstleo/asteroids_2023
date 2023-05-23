using Asteroids.Infrastructure.VContainerExtensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.GameMechanics.Enemies
{
    /// <summary>
    /// Initialize enemy factories and spawning
    /// </summary>
    public class EnemiesScope : LifetimeScope
    {
        
        //TODO: Replace with Addressables
        [SerializeField] private AsteroidsSettingSO _asteroidsSettings;

        [SerializeField] private EnemyWavesSettingsSO _waves;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_asteroidsSettings.Settings).AsSelf();
            builder.RegisterInstance(_waves.Waves).AsImplementedInterfaces();
            builder.Register<EnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<FieldEnemySpawner>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterNonLazy<EnemyWavesGenerator>(director =>
            {
                director.SpawnNextWave();
            });
        }


    }
}
