using System;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Components.Weapons;
using Asteroids.Infrastructure.Update;
using UnityEngine;

namespace Asteroids.GameMechanics.Components.Input
{
   /// <summary>
   /// Converts button presses to inputs observables
   /// </summary>
    public class PlayerKeyboardInput : IMoveInputProvider, IWeaponInputProvider, ITickListener
    {
        private readonly IKeyboardInput _input;

        private readonly ReactiveProperty<RotationInput> _turnInput = new();
        private readonly ReactiveProperty<bool> _thrust = new();
        private readonly ReactiveProperty<bool> _shot = new();

        public IObservable<bool> Thrust => _thrust;
        public IObservable<RotationInput> TurnInput => _turnInput;
        
        public IObservable<bool> Shot => _shot;
    
        public PlayerKeyboardInput(IKeyboardInput input)
        {
            _input = input;
        }

        private bool IsThrustPressed()
        {
            return _input.GetKey(KeyCode.W) || _input.GetKey(KeyCode.UpArrow);   
        }

        private bool IsShotPressed()
        {
            return _input.GetKey(KeyCode.Space);
        }

        private RotationInput GetRotationInput()
        {
            var input = RotationInput.None;
            if (_input.GetKey(KeyCode.A) || _input.GetKey(KeyCode.LeftArrow))
            {
                input += (sbyte)RotationInput.Left;
            }
            if (_input.GetKey(KeyCode.D) || _input.GetKey(KeyCode.RightArrow))
            {
                input += (sbyte)RotationInput.Right;
            }
            return input;
        }
        
        public void Tick(float deltaTime)
        {
            _turnInput.Value = GetRotationInput();
            _thrust.Value = IsThrustPressed();
            _shot.Value = IsShotPressed();
        }
    }

}
