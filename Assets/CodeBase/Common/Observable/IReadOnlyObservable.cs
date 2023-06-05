using System;

namespace Common.Observable
{
    public interface IReadOnlyObservable<out T>
    {
        event Action<T> Changed;
        T Value { get; }    
    }
}