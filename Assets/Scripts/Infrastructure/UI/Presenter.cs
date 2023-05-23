using System;

namespace Asteroids.Infrastructure.UI
{
    public class Presenter : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        protected void ToDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
