using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.GameMechanics.Entities;
using Asteroids.GameMechanics.Player;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Scores
{
    public class PlayerBuildDirectorTest
    {
        private PlayerBuildDirector _playerBuilder;
        private IFactory<string, EntityBuilder> _defaultBuilder;
        private GameEntity _entity;
        private IFactory<GameEntity> _projectileBuilder;
        private IMoveInputProvider _moveInput;
        private IWeaponInputProvider _weaponInput;
        private IPlayerSettings _playerSettings;
        private const string PrefabName = "Player";

        [SetUp]
        public void Setup()
        {
            _moveInput = Substitute.For<IMoveInputProvider>();
            _weaponInput = Substitute.For<IWeaponInputProvider>();
            _playerSettings = Substitute.For<IPlayerSettings>();
            
            _playerSettings.PrefabId.Returns(PrefabName);
            _playerSettings.WeaponCooldown.Returns(0.5f);
            _playerSettings.PlayerAccelerationsSettings.Returns(new AccelerationSettings());
            
            _projectileBuilder = Substitute.For<IFactory<GameEntity>>();
            _defaultBuilder = Substitute.For<IFactory<string, EntityBuilder>>();
            _defaultBuilder.Create(Arg.Any<string>()).Returns(new EntityBuilder());
            
            _playerBuilder = new PlayerBuildDirector(_defaultBuilder, _moveInput, _weaponInput, _playerSettings, _projectileBuilder);
            _entity = _playerBuilder.Create();
        }

        [Test]
        public void Creates_from_default()
        {
            _defaultBuilder.Received().Create(PrefabName);
        }
        
        [Test]
        public void Has_accelerator()
        {
            Assert.IsNotNull(_entity.GetComponent<Accelerator>());
        }
        
        [Test]
        public void Has_weapon()
        {
            Assert.IsNotNull(_entity.GetComponent<ProjectileWeapon>());
        }
    }
}
