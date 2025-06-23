using System;
using System.Collections.Generic;
/// <summary>
/// Provides an observable pattern implementation for notifying subscribers about changes or events.
/// This class allows multiple listeners to subscribe to notifications and be informed when an event occurs.
/// T represents the type of data that will be passed to the listeners when an event is triggered.
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
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
