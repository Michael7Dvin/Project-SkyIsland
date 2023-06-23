using Common.Observable;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Infrastructure.Services.Pausing
{
    public class PauseService : IPauseService
    {
        private const float PausedTimeScale = 0f;
        private const float NotPausedTimeScale = 1f;

        private readonly Observable<bool> _paused = new();

        private readonly IUpdater _updater;

        public PauseService(IUpdater updater)
        {
            _updater = updater;
        }
        
        public IReadOnlyObservable<bool> Paused => _paused; 

        public void Pause()
        {
            _updater.StopUpdating();
            Time.timeScale = PausedTimeScale;
            _paused.Value = true;
        }

        public void Resume()
        {
            _updater.StartUpdating();
            Time.timeScale = NotPausedTimeScale;
            _paused.Value = false;
        }
    }
}