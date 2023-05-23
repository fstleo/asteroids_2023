namespace Asteroids.Infrastructure
{
    public interface IStorage<T>
    {
        public T Load();
        public void Save(T value);
    }
}