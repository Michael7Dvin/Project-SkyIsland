using Common.Observable;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Services.Pause
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
            _paused.Value = true;
            Time.timeScale = PausedTimeScale;
            _updater.StopUpdating();
        }

        public void Resume()
        {
            _paused.Value = false;
            Time.timeScale = NotPausedTimeScale;
            _updater.StartUpdating();
        }
    }
}