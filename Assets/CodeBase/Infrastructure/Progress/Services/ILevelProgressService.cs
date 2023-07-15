namespace Infrastructure.Progress.Services
{
    public interface ILevelProgressService
    {
        void SetCurrentProgress(AllProgress progress);

        void SaveCurrentProgress();
        void LoadCurrentProgress();
    }
}