using System;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.LevelLoading;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Progress
{
    [Serializable]
    public class AllProgress
    {
        public readonly SaveSlot SaveSlot;
        
        public LevelData CurrentLevel;

        public HeroProgress HeroProgress;
        public IslandLevelProgress IslandLevelProgress;

        public AllProgress(SaveSlot saveSlot)
        {
            SaveSlot = saveSlot;

            CurrentLevel = new LevelData(LevelType.Island, SceneType.Island);
            HeroProgress = new HeroProgress();
            IslandLevelProgress = new IslandLevelProgress();
        }
    }
}