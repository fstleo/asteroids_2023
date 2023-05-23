using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Shooting
{

    public class ProjectileWeaponTest
    {
        private ProjectileWeapon _weapon;
        private IFactory<GameEntity> _projectileFactory;
        private GameEntity _shooter;
        private GameEntity _projectile;

        [SetUp]
        public void Setup()
        {
            _shooter = new GameEntity();

            _projectile = new GameEntity();
            _projectileFactory = Substitute.For<IFactory<GameEntity>>();
            _projectileFactory.Create().Returns(_projectile);
            
            _weapon = new ProjectileWeapon(_shooter, .5f, _projectileFactory);
        }
        
        [Test]
        public void Projectile_was_initialized()
        {
            _shooter.Position.Value = new Vector2(5f, 0);
            _shooter.Angle.Value = 10f;

            _weapon.Shot(true);

            _projectileFactory.Received().Create();
            Assert.AreEqual(_shooter.Position.Value, _projectile.Position.Value);
            Assert.Less(Mathf.Abs(_shooter.Angle.Value - _projectile.Angle.Value), Mathf.Epsilon);
        }
        
        
        [Test]
        public void Cant_shoot_during_cooldown()
        {
            _weapon.Shot(true);
            _weapon.Tick(0.25f);
            _weapon.Shot(true);
            
            _projectileFactory.Received(1).Create();
        }
        
        [Test]
        public void Can_shoot_after_cooldown()
        {
            _weapon.Shot(true);
            _weapon.Tick(0.75f);
            _weapon.Shot(true);
            
            _projectileFactory.Received(2).Create();
        }
    }


}
