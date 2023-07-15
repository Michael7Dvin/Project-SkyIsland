using System.IO;
using Infrastructure.Progress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly string _savesDrectory = Application.persistentDataPath;
        
        public void Save(AllProgress progress)
        {
            SaveSlot saveSlot = progress.SaveSlot;
            
            string filePath = GetSaveFilePath(saveSlot);
            string json = JsonUtility.ToJson(progress);
            
            File.WriteAllText(filePath, json);
        }

        public bool TryLoad(SaveSlot saveSlot, out AllProgress result)
        {
            string filePath = GetSaveFilePath(saveSlot);

            if (File.Exists(filePath) == true)
            {
                string json = File.ReadAllText(filePath);
                result = JsonUtility.FromJson<AllProgress>(json);
                return true;
            }
            
            result = null;
            return false;
        }

        private string GetSaveFilePath(SaveSlot saveSlot)
        {
            string fileName = $"Save_{saveSlot.ToString()}.json";
            return Path.Combine(_savesDrectory, fileName);
        }
    }
}