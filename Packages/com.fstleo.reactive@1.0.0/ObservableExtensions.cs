using System;
using System;

public static class ObservableExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> method)
    {
        return observable?.Subscribe(Observer<T>.FromMethod(method));
    }

}