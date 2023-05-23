using Asteroids.GameMechanics.Components.Input;
using Asteroids.GameMechanics.Components.Moving;
using NUnit.Framework;

namespace Tests.GameMechanics.PlayerTests
{
    public class GoForwardInputTest
    {
        private GoForwardInput _go;

        [SetUp]
        public void Setup()
        {
            _go = new();
        }
        
        [Test]
        public void Test_if_always_forward()
        {
            var forward = false;
            _go.Thrust.Subscribe(thrust => forward = thrust);
            Assert.IsTrue(forward);
        }
        
        [Test]
        public void Test_rotation()
        {
            var rotation = RotationInput.None;
            _go.TurnInput.Subscribe(turn => rotation = turn);
            Assert.AreEqual(RotationInput.None, rotation);
        }
    }
}
