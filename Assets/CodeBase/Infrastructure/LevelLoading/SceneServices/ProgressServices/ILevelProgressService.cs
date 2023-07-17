using Cysharp.Threading.Tasks;
using Infrastructure.Progress;

namespace Infrastructure.LevelLoading.SceneServices.ProgressServices
{
    public interface ILevelProgressService
    {
        AllProgress CurrentProgress { get; }
        void SetCurrentProgress(AllProgress progress);

        UniTask SaveCurrentProgress();
        void LoadCurrentProgress();
    }
}