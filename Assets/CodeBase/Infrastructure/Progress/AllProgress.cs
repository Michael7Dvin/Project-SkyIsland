using System;
using System.Globalization;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Progress
{
    [Serializable]
    public class AllProgress
    {
        public readonly SaveSlotID SaveSlotID;

        public SceneType CurrentScene;
        public string LastSaveDateTime;
        
        public HeroProgress HeroProgress;
        public IslandWorldProgress IslandWorldProgress;

        public AllProgress(SaveSlotID saveSlotID)
        {
            SaveSlotID = saveSlotID;

            CurrentScene = SceneType.Island;
            LastSaveDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            
            HeroProgress = new HeroProgress();
            IslandWorldProgress = new IslandWorldProgress();
        }
    }
}