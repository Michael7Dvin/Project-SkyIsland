using Common.Observable;

namespace Gameplay.BodyEnvironmentObserving
{
    public interface IBodyEnvironmentObserver
    {
        IReadOnlyObservable<BodyEnvironmentType> EnvironmentType { get; }
    }
}