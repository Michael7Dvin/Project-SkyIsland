using Cysharp.Threading.Tasks;
using Infrastructure.Progress;

namespace Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        UniTask Save(AllProgress progress);
        UniTask<(bool isSuccessful, AllProgress result)> TryLoad(SaveSlotID saveSlotID);
        void DeleteSaveFile(SaveSlotID saveSlotID);
    }
}