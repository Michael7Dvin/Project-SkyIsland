using Infrastructure.Services.Logger;

namespace Infrastructure.Services.Updater
{
    public class UpdatesListenerForDebug
    {
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;
        
        public UpdatesListenerForDebug(IUpdater updater, ICustomLogger logger)
        {
            _updater = updater;
            _logger = logger;

            _updater.Updated += OnUpdate;
            _updater.FixedUpdated += OnFixedUpdate;
            _updater.LateUpdated += OnLateUpdate;
        }

        public void Dispose()
        {
            _updater.Updated -= OnUpdate;
            _updater.FixedUpdated -= OnFixedUpdate;
            _updater.LateUpdated -= OnLateUpdate;
        }
        
        private void OnUpdate(float deltaTime) =>
            _logger.Log("Updated");        
        
        private void OnFixedUpdate(float fixedDeltaTime) =>
            _logger.Log("Fixed Updated");

        private void OnLateUpdate(float deltaTime) =>
            _logger.Log("Late Update");
    }
}