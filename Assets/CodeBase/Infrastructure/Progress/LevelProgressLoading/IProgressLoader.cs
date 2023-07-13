using Infrastructure.Services.LevelLoading;
using Infrastructure.Services.LevelLoading.Data;
using Zenject;

namespace Infrastructure.Progress.LevelProgressLoading
{
    public interface IProgressLoader : IInitializable
    {
        LevelType LevelType { get; }
        void InitializeProgressHandlers(LevelData levelData);
        void RegisterProgressHandlers();
    }
}