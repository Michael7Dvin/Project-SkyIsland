using Gameplay.BodyEnvironmentObserving.GroundDetection;

namespace Gameplay.BodyEnvironmentObserving
{
    public interface IBodyEnvironmentObserverFactory
    {
        IBodyEnvironmentObserver Create(IGroundDetector groundDetector);
    }
}