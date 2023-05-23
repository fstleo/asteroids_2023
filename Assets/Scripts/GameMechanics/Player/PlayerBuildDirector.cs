using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;

namespace Asteroids.GameMechanics.Player
{

    /// <summary>
    /// Builds player entity
    /// </summary>
    public class PlayerBuildDirector : IFactory<GameEntity>
    {
        private readonly EntityBuilder _playerEntityBuilder;
        
        public PlayerBuildDirector(IFactory<string, EntityBuilder> defaultEntity, IMoveInputProvider playerInput, IWeaponInputProvider weaponInputProvider,
                                   IPlayerSettings playerSettingsSo, IFactory<GameEntity> playerProjectileBuilder)
        {
            _playerEntityBuilder = defaultEntity.Create(playerSettingsSo.PrefabId)
                                                .WithProjectileWeapon(weaponInputProvider, playerSettingsSo.WeaponCooldown,  playerProjectileBuilder)
                                                .AddControls(playerInput, playerSettingsSo.PlayerAccelerationsSettings);

        }

        public GameEntity Create()
        {
            return _playerEntityBuilder.Create();
        }
    }
}