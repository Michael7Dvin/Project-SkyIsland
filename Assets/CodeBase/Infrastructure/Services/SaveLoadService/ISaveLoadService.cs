using Infrastructure.Progress;

namespace Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void Save(AllProgress progress);
        bool TryLoad(SaveSlot saveSlot, out AllProgress result);
    }
}