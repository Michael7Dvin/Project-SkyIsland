using Common.Observable;
using Infrastructure.Services.Updater;

namespace Gameplay.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly Observable<bool> _paused = new();

        private readonly IUpdater _updater;

        public PauseService(IUpdater updater)
        {
            _updater = updater;
        }
        
        public IReadOnlyObservable<bool> Paused => _paused; 

        public void Pause()
        {
            _paused.Value = true;
            _updater.StopUpdating();
        }

        public void Resume()
        {
            _paused.Value = false;
            _updater.StartUpdating();
        }
    }
}