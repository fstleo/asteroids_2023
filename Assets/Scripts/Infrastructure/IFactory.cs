namespace Asteroids.Infrastructure
{
    public interface IFactory<in TIn, out TOut>
    {
        TOut Create(TIn parameters);
    }

    public interface IFactory<out T>
    {
        T Create();
    }


}

