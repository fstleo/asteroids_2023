using System.Collections;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Entities;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.Moving
{

    public class AcceleratorTest
    {
        private Accelerator _accelerator;
        private GameEntity _entity;

        [SetUp]
        public void Setup()
        {
            _entity = new GameEntity();
            _accelerator = new Accelerator(_entity,
                                           new AccelerationSettings
                                           {
                                               MaximumSpeed = 6f,
                                               Acceleration = 4f,
                                               RotationSpeed = 1f
                                           });
        }
        
        public struct ThrustTestData
        {
            public Vector2 ExpectedVelocity;
            public float ThrustTime;
        
            public override string ToString()
            {
                return $"Thrust: {ThrustTime} second(s), velocity is {ExpectedVelocity}";
            }
        }
        
        private static IEnumerable ThrustTestCases 
        {
            get
            {
                return new[]
                {
                    new ThrustTestData
                    {
                        ExpectedVelocity = new Vector2(0, 4f),
                        ThrustTime = 1f,
                    },
                    new ThrustTestData
                    {
                        ExpectedVelocity = new Vector2(0, 2f),
                        ThrustTime = 0.5f,
                    },
                    new ThrustTestData
                    {
                        ExpectedVelocity = new Vector2(0, 6),
                        ThrustTime = 2f,
                    }
                }.ToTestCases();
            }
        }
        
        [Test]
        [TestCaseSource(nameof(ThrustTestCases))]
        public void Thrust_changes_velocity_according_to_acceleration(ThrustTestData testData)
        {
            _accelerator.SetThrust(true);
        
            _accelerator.Tick(testData.ThrustTime);
        
            Assert.AreEqual(testData.ExpectedVelocity, _entity.Velocity.Value);
        }
        
        [Test]
        public void Velocity_clamped_to_max()
        {
            var expectedVelocity = 6f;
            _accelerator.SetThrust(true);
        
            _accelerator.Tick(16f);
        
            Assert.AreEqual(expectedVelocity, _entity.Velocity.Value.magnitude);
        }
        
        
        public struct RotationTestData
        {
            public float ExpectedRotationSpeed;
            public RotationInput Input;
        
            public override string ToString()
            {
                return Input == RotationInput.None 
                    ? "Do nothing, angle should stay the same" 
                    : $"Rotation speed should be {ExpectedRotationSpeed}";
            }
        }
        
        private static IEnumerable RotationTestCases 
        {
            get
            {
                return new[]
                {
                    new RotationTestData
                    {
                        Input = RotationInput.Left,
                        ExpectedRotationSpeed = 1f,
                    },
                    new RotationTestData
                    {
                        Input = RotationInput.None,
                        ExpectedRotationSpeed = 0f,
                    },
                    new RotationTestData
                    {
                        Input = RotationInput.Left,
                        ExpectedRotationSpeed = 1f,
                    },
                }.ToTestCases();
            }
        }
        
        [Test]
        [TestCaseSource(nameof(RotationTestCases))]
        public void Set_rotation_rotation_speed(RotationTestData rotationData)
        {
            _accelerator.SetRotationInput(rotationData.Input);
        
            _accelerator.Tick(0.1f);
        
            Assert.AreEqual(rotationData.ExpectedRotationSpeed, _entity.RotationSpeed.Value);
        }
        
        
        public struct DecelerationTestCaseData
        {
            public Vector2 ExpectedVelocity;
            public float ThrustTime;
            public float NoThrustTime;
        
            public override string ToString()
            {
                return $"Thrust for {ThrustTime} second(s), then no thrust for {NoThrustTime} second(s), expected position: {ExpectedVelocity}";
            }
        }
        
        private static IEnumerable DecelerationTestCases 
        {
            get
            {
                return new[]
                {
                    new DecelerationTestCaseData
                    {
                        ExpectedVelocity = new Vector2(0, 0),
                        ThrustTime = 1f,
                        NoThrustTime = 1f
                    },
        
                    new DecelerationTestCaseData
                    {
                        ExpectedVelocity = new Vector2(0, 4f),
                        ThrustTime = 1f,
                        NoThrustTime = 0f
                    },
        
                    new DecelerationTestCaseData
                    {
                        ExpectedVelocity = new Vector2(0,0),
                        ThrustTime = 1f,
                        NoThrustTime = 2f
                    }
                }.ToTestCases();
            }
        }
        
        [Test]
        [TestCaseSource(nameof(DecelerationTestCases))]
        public void Drag_applied_if_there_is_no_thrust(DecelerationTestCaseData testCaseData)
        {
        
            _accelerator.SetThrust(true);
        
            _accelerator.Tick(testCaseData.ThrustTime);
        
            _accelerator.SetThrust(false);
        
            _accelerator.Tick(testCaseData.NoThrustTime);
        
            Assert.AreEqual(testCaseData.ExpectedVelocity, _entity.Velocity.Value);
        }
        
    }
}
