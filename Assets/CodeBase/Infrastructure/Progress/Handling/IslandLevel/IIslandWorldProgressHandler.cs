namespace Infrastructure.Progress.Handling.IslandLevel
{
    public interface IIslandWorldProgressHandler
    {
        void WriteProgress(IslandWorldProgress progress);
        void LoadProgress(IslandWorldProgress progress);
    }
}