namespace Asteroids.Infrastructure
{
    public interface IPool<T>
    {
        bool TryGet(out T item);
        void Return(T item);
    }
}