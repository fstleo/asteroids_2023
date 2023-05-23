using Asteroids.GameMechanics.Scores;
using Asteroids.Infrastructure;
using Asteroids.Infrastructure.VContainerExtensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.GameMechanics
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Bounds2D _worldBounds;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MemoryPool<GameObject>>(Lifetime.Singleton).AsImplementedInterfaces();
            
            //TODO: add pool decorator
            builder.Register<AddressableFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<AsteroidField.AsteroidField>(Lifetime.Singleton).WithParameter(_worldBounds).AsImplementedInterfaces();
            builder.Register<DefaultEntityBuilder>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PlayerPrefStorage>(Lifetime.Singleton).WithParameter("TopScore").AsImplementedInterfaces();
            builder.RegisterNonLazy<ScoreService>().AsImplementedInterfaces().AsSelf();
        }
    }

}