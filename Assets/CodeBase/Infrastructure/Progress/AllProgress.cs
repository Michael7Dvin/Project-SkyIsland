using System;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Progress
{
    [Serializable]
    public class AllProgress
    {
        public readonly SaveSlot SaveSlot;

        public SceneType CurrentScene;

        public HeroProgress HeroProgress;
        public IslandWorldProgress IslandWorldProgress;

        public AllProgress(SaveSlot saveSlot)
        {
            SaveSlot = saveSlot;

            CurrentScene = SceneType.Island;
            HeroProgress = new HeroProgress();
            IslandWorldProgress = new IslandWorldProgress();
        }
    }
}