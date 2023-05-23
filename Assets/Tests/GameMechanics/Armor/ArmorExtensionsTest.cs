using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Entities;
using NUnit.Framework;

namespace Tests.GameMechanics.Armor
{
    public class ArmorExtensionsTest
    {
        [Test]
        public void Entity_build_adds_armor()
        {
            var entity = new EntityBuilder().WithOneHitArmor().Create();
            Assert.IsNotNull(entity.GetComponent<IArmor>());
        }
    }
}
