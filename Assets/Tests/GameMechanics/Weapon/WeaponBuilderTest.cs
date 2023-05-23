using System;
using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Shooting
{
    public class WeaponBuilderTest
    {
        [Test]
        public void Weapon_added_to_entity()
        {
            var inputProvider = Substitute.For<IWeaponInputProvider>();
            inputProvider.Shot.Returns(Substitute.For<IObservable<bool>>());
            var projectileFactory = Substitute.For<IFactory<GameEntity>>();
            var entity = new EntityBuilder().WithProjectileWeapon(inputProvider, 2f, projectileFactory).Create();
            
            Assert.IsNotNull(entity.GetComponent<ProjectileWeapon>());
            inputProvider.Shot.Received().Subscribe(Arg.Any<IObserver<bool>>());
        }
    }
}
