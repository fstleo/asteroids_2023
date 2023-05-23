using System;

namespace Asteroids.GameMechanics
{
    public interface IView<in T> 
    {
        IDisposable Init(T data);
    }
}