using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.Infrastructure.Update;

namespace Asteroids.Infrastructure
{

    public class TaskWaiter : IWaiter, ITickListener
    {
        private class Waiter : ITickListener
        {
            private float _timeLeft;
            private TaskCompletionSource<bool> _currentTask; 

            public Task Wait(float time)
            {
                _timeLeft = time;
                _currentTask = new TaskCompletionSource<bool>();
                return _currentTask.Task;
            }

            public void Tick(float deltaTime)
            {
                _timeLeft -= deltaTime;
                if (_timeLeft < 0)
                {
                    _currentTask.SetResult(true);    
                }
            }
        }

        private readonly List<Waiter> _activeWaiters = new(8);
        private readonly IPool<Waiter> _waitersPool = new MemoryPool<Waiter>();

        public async Task Wait(int seconds)
        {
            if (!_waitersPool.TryGet(out var waiter))
            {
                waiter = new Waiter();
            }
            _activeWaiters.Add(waiter);
            await waiter.Wait(seconds);
            _activeWaiters.Remove(waiter);
            _waitersPool.Return(waiter);
        }

        public void Tick(float deltaTime)
        {
            for (var index = 0; index < _activeWaiters.Count; index++)
            {
                _activeWaiters[index].Tick(deltaTime);
            }
        }
    }
}
