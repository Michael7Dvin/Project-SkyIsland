using System.IO;
using Cysharp.Threading.Tasks;
using Infrastructure.Progress;
using Infrastructure.Services.Logging;
using UnityEngine;

namespace Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly string _savesDrectory = Application.persistentDataPath;

        private readonly ICustomLogger _logger;

        public SaveLoadService(ICustomLogger logger)
        {
            _logger = logger;
        }

        public async UniTask Save(AllProgress progress)
        {
            SaveSlotID saveSlotID = progress.SaveSlotID;
            
            string filePath = GetSaveFilePath(saveSlotID);
            string json = JsonUtility.ToJson(progress);
            
            await File.WriteAllTextAsync(filePath, json);
            _logger.Log($"Progress file saved: {filePath}");
        }

        public async UniTask<(bool isSuccessful, AllProgress result)> TryLoad(SaveSlotID saveSlotID)
        {
            string filePath = GetSaveFilePath(saveSlotID);

            if (File.Exists(filePath) == true)
            {
                string json = await File.ReadAllTextAsync(filePath);
                _logger.Log($"Progress file loaded: {filePath}");
                
                AllProgress progress = JsonUtility.FromJson<AllProgress>(json);
                return (true, progress);
            }
            
            return (false, null);
        }

        public void DeleteSaveFile(SaveSlotID saveSlotID)
        {
            string filePath = GetSaveFilePath(saveSlotID);
            File.Delete(filePath);
        }

        private string GetSaveFilePath(SaveSlotID saveSlotID)
        {
            string fileName = $"Save_{saveSlotID.ToString()}.json";
            return Path.Combine(_savesDrectory, fileName);
        }
    }
}