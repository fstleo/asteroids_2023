using System;
using UnityEngine;

namespace Asteroids.GameMechanics.Components.Armor
{
    public class ArmorComponent : MonoBehaviour, IView<IArmor>, IDisposable
    {
        private IArmor _armor;

        public IDisposable Init(IArmor armor)
        {
            _armor = armor;
            return this;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            _armor?.Hit();
        }

        public void Dispose()
        {
            _armor = null;
        }
    }
}