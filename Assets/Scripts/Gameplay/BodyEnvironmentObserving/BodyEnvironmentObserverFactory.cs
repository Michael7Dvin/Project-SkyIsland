using Gameplay.BodyEnvironmentObserving.GroundDetection;

namespace Gameplay.BodyEnvironmentObserving
{
    public class BodyEnvironmentObserverFactory : IBodyEnvironmentObserverFactory
    {
        public IBodyEnvironmentObserver Create(IGroundDetector groundDetector) => 
            new BodyEnvironmentObserver(groundDetector);
    }
}