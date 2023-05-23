using Asteroids.Game;
using Asteroids.Infrastructure;
using Asteroids.Infrastructure.Update;
using Asteroids.Infrastructure.VContainerExtensions;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AsteroidsGameApp>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TaskWaiter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameStateKeeper>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<VContainerUpdateRunner>();
            builder.RegisterNonLazy<ApplicationStateListener>();
        }
    }
}