using Gameplay.Heros;

namespace Infrastructure.Progress.Handling.IslandLevel
{
    public class IslandLevelProgressHandler : IProgressHandler
    {
        public ISavableHero Hero { get; private set; }
        private IslandLevelProgress _progress;

        public void Init(IslandLevelProgress progress) => 
            _progress = progress;

        public void RegisterHero(ISavableHero hero) => 
            Hero = hero;

        public void SetValuesFromProgress()
        {
            if (_progress.IsEmpty == false)
            {
                Hero.Position = _progress.HeroPosition;
                Hero.Rotation = _progress.HeroRotation;
            }
        }

        public void WriteValuesToProgress()
        {
            _progress.IsEmpty = false;

            _progress.HeroPosition = Hero.Position;
            _progress.HeroRotation = Hero.Rotation;
        }
    }
}