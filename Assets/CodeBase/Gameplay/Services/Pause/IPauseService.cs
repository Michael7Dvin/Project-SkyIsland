using Common.Observable;

namespace Gameplay.Services.Pause
{
    public interface IPauseService
    {
        IReadOnlyObservable<bool> Paused { get; }

        void Pause();
        void Resume();
    }
}