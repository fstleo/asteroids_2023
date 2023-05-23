using System.Threading.Tasks;

namespace Asteroids.Infrastructure
{
    public interface IWaiter
    {
        Task Wait(int seconds);
    }

}