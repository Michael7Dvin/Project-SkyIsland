using Common.Observable;

namespace Infrastructure.Services.Pausing
{
    public interface IPauseService
    {
        IReadOnlyObservable<bool> Paused { get; }

        void Pause();
        void Resume();
    }
}