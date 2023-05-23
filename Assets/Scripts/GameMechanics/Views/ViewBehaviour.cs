using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.GameMechanics.Views
{
    public abstract class ViewBehaviour<T> : MonoBehaviour, IView<T>
    {
        public IDisposable Init(T data)
        {
            var disposable = new CompositeDisposable(GetSubscriptions(data));
            return disposable;
        }

        protected abstract IEnumerable<IDisposable> GetSubscriptions(T data);
        
    }
}