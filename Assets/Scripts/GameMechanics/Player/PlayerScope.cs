using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.GameMechanics.Player.PlayerLife;
using Asteroids.Infrastructure.VContainerExtensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.GameMechanics.Player
{

    /// <summary>
    /// Initialize player's entity and life counting service
    /// </summary>
    public class PlayerScope : LifetimeScope
    {
        
        //TODO: replace with Addressables        
        [SerializeField] private PlayerSettingsSO _playerSettingsSO;
    
        [SerializeField] private PlayerLifeSettingsSO _playerLifeSettingsSO;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ProjectileBuilder>(Lifetime.Scoped)
                   .WithParameter(_playerSettingsSO.PrefabId + "_Projectile")
                   .WithParameter(_playerSettingsSO.ProjectileLifetime)
                   .WithParameter(_playerSettingsSO.ProjectileSettings).AsSelf();
            builder.Register<PlayerBuildDirector>(resolver => new PlayerBuildDirector(resolver.Resolve<DefaultEntityBuilder>(),
                                                                                      resolver.Resolve<IMoveInputProvider>(),
                                                                                      resolver.Resolve<IWeaponInputProvider>(),
                                                                                      _playerSettingsSO, 
                                                                                      resolver.Resolve<ProjectileBuilder>()), Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.RegisterInstance(_playerLifeSettingsSO.PlayerLifeSettings).AsImplementedInterfaces();
            builder.RegisterNonLazy<PlayerLifeService>(lifeService => lifeService.SpawnPlayer()).AsImplementedInterfaces().AsSelf();
        }
    }

}