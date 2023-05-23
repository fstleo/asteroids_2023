using System;
using System;

public class ReactiveProperty<T> : IObservable<T> 
{
    private event Action<T> Changed;
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value))
            {
                return;
            }
            _value = value;
            Changed?.Invoke(_value);
        }
    }

    public ReactiveProperty(T value)
    {
        _value = value;
    }

    public ReactiveProperty()
    {
        _value = default;
    }

    private class Unsubscriber : IDisposable
    {
        private readonly Action _executeOnDispose;

        public Unsubscriber(Action executeOnDispose)
        {
            _executeOnDispose = executeOnDispose;

        }

        public void Dispose()
        {
            _executeOnDispose?.Invoke();
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        observer.OnNext(_value);
        Changed += observer.OnNext;
        return new Unsubscriber(() => Changed -= observer.OnNext);
    }
}