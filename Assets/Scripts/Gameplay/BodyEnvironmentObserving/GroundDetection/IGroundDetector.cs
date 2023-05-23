using Common.Observable;

namespace Gameplay.BodyEnvironmentObserving.GroundDetection
{
    public interface IGroundDetector
    {
        IReadOnlyObservable<bool> Grounded { get; }
    }
}