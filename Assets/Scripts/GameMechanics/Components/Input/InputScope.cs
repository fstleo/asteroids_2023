using Asteroids.Infrastructure.Update;
using VContainer;
using VContainer.Unity;

namespace Asteroids.GameMechanics.Components.Input
{
    public class InputScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<UnityInputWrapper>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerKeyboardInput>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PauseListener>();
            builder.RegisterBuildCallback(resolver => resolver.Resolve<ITickSource>().AddListener(resolver.Resolve<PlayerKeyboardInput>()));
        }
    }
}
