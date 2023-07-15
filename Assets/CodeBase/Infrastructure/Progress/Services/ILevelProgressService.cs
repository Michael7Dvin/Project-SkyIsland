using Cysharp.Threading.Tasks;

namespace Infrastructure.Progress.Services
{
    public interface ILevelProgressService
    {
        void SetCurrentProgress(AllProgress progress);

        UniTask SaveCurrentProgress();
        void LoadCurrentProgress();
    }
}