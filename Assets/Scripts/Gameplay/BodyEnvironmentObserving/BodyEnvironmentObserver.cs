using Common.Observable;

namespace Gameplay.BodyEnvironmentObserving
{
    public class BodyEnvironmentObserver : IBodyEnvironmentObserver
    {
        private readonly Observable<BodyEnvironmentType> _environmentType = new();

        public void SetBodyEnvironmentTypeForDebug(BodyEnvironmentType type) =>
            _environmentType.Value = type;

        public IReadOnlyObservable<BodyEnvironmentType> EnvironmentType => _environmentType;
    }
}