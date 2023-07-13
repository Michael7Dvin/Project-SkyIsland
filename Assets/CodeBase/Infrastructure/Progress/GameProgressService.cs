using System.Collections.Generic;
using Infrastructure.Progress.Handling;
using Infrastructure.Services.SaveLoadService;
using UnityEngine;

namespace Infrastructure.Progress
{
    public class GameProgressService : IGameProgressService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly List<IProgressHandler> _progressHandlers = new();

        public GameProgressService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public AllProgress CurrentProgress { get; private set; }

        public void SetCurrentProgress(AllProgress progress) => 
            CurrentProgress = progress;
        
        public void SaveCurrentProgress()
        {
            foreach (IProgressHandler progressHandler in _progressHandlers) 
                progressHandler.WriteValuesToProgress();

            _saveLoadService.Save(CurrentProgress.SaveSlot, CurrentProgress);
        }

        public void LoadCurrentProgress()
        {
            foreach (IProgressHandler progressHandler in _progressHandlers) 
                progressHandler.SetValuesFromProgress();
        }

        public void RegisterProgressHandler(IProgressHandler handler) => 
            _progressHandlers.Add(handler);

        public void ClearRegisteredProgressHandlers() => 
            _progressHandlers.Clear();
    }
}