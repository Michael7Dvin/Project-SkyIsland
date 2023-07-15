namespace Infrastructure.Progress.Handling.Heros
{
    public interface IHeroProgressHandler
    {
        void WriteProgress(HeroProgress progress);
        void LoadProgress(HeroProgress progress);
    }
}