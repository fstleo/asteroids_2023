using System.Threading.Tasks;

namespace Asteroids.Infrastructure
{
    public interface IProvider<T>
    {
        Task<T> GetAsync(string id);
        void Release(T item);
    }
}