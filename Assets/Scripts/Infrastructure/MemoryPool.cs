using System.Collections.Generic;

namespace Asteroids.Infrastructure
{
    public class MemoryPool<T> : IPool<T>
    {
        private readonly Stack<T> _items = new(10);

        public bool TryGet(out T item)
        {
            return _items.TryPop(out item);
        }

        public void Return(T item)
        {
            _items.Push(item);
        }
    }
}