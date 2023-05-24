using System;
using Common.Observable;

namespace Gameplay.BodyEnvironmentObserving
{
    public interface IBodyEnvironmentObserver : IDisposable
    {
        IReadOnlyObservable<BodyEnvironmentType> EnvironmentType { get; }
    }
}