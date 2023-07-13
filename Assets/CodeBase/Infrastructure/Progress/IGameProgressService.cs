using Infrastructure.Progress.Handling;

namespace Infrastructure.Progress
{
    public interface IGameProgressService
    {
        AllProgress CurrentProgress { get; }

        void SetCurrentProgress(AllProgress progress);

        void SaveCurrentProgress();
        void LoadCurrentProgress();
        
        void RegisterProgressHandler(IProgressHandler handler);
        void ClearRegisteredProgressHandlers();
    }
}