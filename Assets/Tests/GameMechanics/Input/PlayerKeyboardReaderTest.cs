using Asteroids.GameMechanics.Components.Input;
using Asteroids.GameMechanics.Components.Moving;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.PlayerTests
{

    public class PlayerKeyboardReaderTest
    {
        private PlayerKeyboardInput _playerInput;
        private IKeyboardInput _systemInput;
        
        [SetUp]
        public void Setup()
        {
            _systemInput = Substitute.For<IKeyboardInput>();
            _playerInput = new PlayerKeyboardInput(_systemInput);
        }
        
        [Test]
        [TestCase(KeyCode.A)]
        [TestCase(KeyCode.LeftArrow)]
        public void Rotate_left_when_left_is_pressed(KeyCode leftCode)
        {
            var input = RotationInput.None;
            _playerInput.TurnInput.Subscribe(turnInput => input = turnInput);
            
            _systemInput.GetKey(Arg.Is(leftCode)).Returns(true);
            _playerInput.Tick(1f);
            
            Assert.AreEqual(RotationInput.Left, input);
        }
        
        [Test]
        [TestCase(KeyCode.D)]
        [TestCase(KeyCode.RightArrow)]
        public void Rotate_right_when_right_is_pressed(KeyCode rightCode)
        {
            var input = RotationInput.None;
            _playerInput.TurnInput.Subscribe(turnInput => input = turnInput);
            
            _systemInput.GetKey(Arg.Is(rightCode)).Returns(true);
            _playerInput.Tick(1f);
            
            Assert.AreEqual(RotationInput.Right, input);
        }
        
        
        [Test]
        [TestCase(KeyCode.A, KeyCode.D)]
        [TestCase(KeyCode.A, KeyCode.RightArrow)]
        [TestCase(KeyCode.LeftArrow, KeyCode.D)]
        [TestCase(KeyCode.LeftArrow, KeyCode.RightArrow)]
        public void No_rotation_when_both_input_is_pressed(KeyCode leftInput, KeyCode rightInput)
        {
            var input = RotationInput.None;
            _playerInput.TurnInput.Subscribe(turnInput => input = turnInput);
            
            _systemInput.GetKey(Arg.Is(leftInput)).Returns(true);
            _systemInput.GetKey(Arg.Is(rightInput)).Returns(true);
            _playerInput.Tick(1f);
            
            Assert.AreEqual(RotationInput.None, input);
        }
        
        [Test]
        [TestCase(KeyCode.A, KeyCode.D)]
        [TestCase(KeyCode.A, KeyCode.RightArrow)]
        [TestCase(KeyCode.LeftArrow, KeyCode.D)]
        [TestCase(KeyCode.LeftArrow, KeyCode.RightArrow)]
        public void No_rotation_when_no_input_is_pressed(KeyCode leftInput, KeyCode rightInput)
        {
            var input = RotationInput.None;
            _playerInput.TurnInput.Subscribe(turnInput => input = turnInput);
            
            _systemInput.GetKey(Arg.Is(leftInput)).Returns(false);
            _systemInput.GetKey(Arg.Is(rightInput)).Returns(false);
            _playerInput.Tick(1f);
            
            Assert.AreEqual(RotationInput.None, input);
        }
        
        [Test]
        [TestCase(KeyCode.W, true)]
        [TestCase(KeyCode.W,false)]
        [TestCase(KeyCode.UpArrow, true)]
        [TestCase(KeyCode.UpArrow,false)]
        public void Thrust_when_forward_is_pressed(KeyCode forwardKey, bool isPressed)
        {
            var thrust = false;
            _playerInput.Thrust.Subscribe(thrustInput => thrust = thrustInput);
            
            _systemInput.GetKey(Arg.Is(forwardKey)).Returns(isPressed);
            
            _playerInput.Tick(1f);
            
            
            Assert.AreEqual(isPressed, thrust);
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Shot_when_shot_is_pressed(bool shotPressed)
        {
            var shot = false;
            _playerInput.Shot.Subscribe(shotInput => shot = shotInput);
            
            _systemInput.GetKey(Arg.Is(KeyCode.Space)).Returns(shotPressed);
            
            _playerInput.Tick(1f);

            Assert.AreEqual(shotPressed, shot);
        }
    }
}
