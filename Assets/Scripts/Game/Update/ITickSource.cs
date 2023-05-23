namespace Asteroids.Infrastructure.Update
{
    public interface ITickSource
    {
        void AddListener(ITickListener listener);
        void RemoveListener(ITickListener listener);
    }
}