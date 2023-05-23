using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;

public class CompositeDisposable : IDisposable
{
    private bool _disposed;
    private readonly List<IDisposable> _disposables;

    public CompositeDisposable()
    {
        _disposables = new List<IDisposable>(4);
    }
        
    public CompositeDisposable(IEnumerable<IDisposable> disposables)
    {
        _disposables = new List<IDisposable>(disposables);
    }

    public void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }
        
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
        _disposables.Clear();
        _disposed = true;
    }
        
}