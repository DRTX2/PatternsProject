using System;
using System.Collections.Generic;

public class Observable<T>
{
    private readonly List<Action<T>> _listeners = new();

    public void Subscribe(Action<T> listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void Unsubscribe(Action<T> listener)
    {
        _listeners.Remove(listener);
    }

    public void Notify(T data)
    {
        foreach (var listener in _listeners)
            listener?.Invoke(data);
    }
}
