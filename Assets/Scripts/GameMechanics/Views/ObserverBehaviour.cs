using System;
using UnityEngine;

namespace Asteroids.GameMechanics.Views
{
    public abstract class ObserverBehaviour<T> : MonoBehaviour, IObserver<T>
    {
        public void OnCompleted() { }
        
        public void OnError(Exception error) {}

        public abstract void OnNext(T value);

    }

}
