using System;
using System;

public class Observer<T> : IObserver<T>
{
    private Action<T> _onNext;
    private Action<Exception> _onError;
    private Action _onCompleted;

    public static IObserver<T> FromMethod(Action<T> onNext)
    {
        return new Observer<T>
        {
            _onNext = onNext
        };
    }

    public void OnCompleted()
    {
        _onCompleted?.Invoke();
    }

    public void OnError(Exception error)
    {
        _onError?.Invoke(error);
    }

    public void OnNext(T value)
    {
        _onNext?.Invoke(value);
    }
}