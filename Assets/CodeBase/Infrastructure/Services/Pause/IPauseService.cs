using Common.Observable;

namespace Infrastructure.Services.Pause
{
    public interface IPauseService
    {
        IReadOnlyObservable<bool> Paused { get; }

        void Pause();
        void Resume();
    }
}