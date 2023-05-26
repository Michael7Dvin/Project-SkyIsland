using Common.Observable;

namespace Gameplay.GroundTypeObserving
{
    public interface IGroundTypeObserver
    {
        public IReadOnlyObservable<GroundType> CurrentGroundType { get; }
    }
}