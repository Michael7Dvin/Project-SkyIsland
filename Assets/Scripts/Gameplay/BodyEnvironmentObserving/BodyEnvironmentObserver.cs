using Common.Observable;
using Gameplay.BodyEnvironmentObserving.GroundDetection;

namespace Gameplay.BodyEnvironmentObserving
{
    public class BodyEnvironmentObserver : IBodyEnvironmentObserver
    {
        private readonly IGroundDetector _groundDetector;
        private readonly Observable<BodyEnvironmentType> _environmentType = new();

        public BodyEnvironmentObserver(IGroundDetector groundDetector)
        {
            _groundDetector = groundDetector;
            _groundDetector.Grounded.Changed += OnGroundedChanged;
        }

        public void Dispose()
        {
            _groundDetector.Grounded.Changed -= OnGroundedChanged;
        }
        
        private void OnGroundedChanged(bool isGrounded)
        {
            if (isGrounded == true)
            {
                _environmentType.Value = BodyEnvironmentType.Grounded;
            }
        }

        public IReadOnlyObservable<BodyEnvironmentType> EnvironmentType => _environmentType;
    }
}