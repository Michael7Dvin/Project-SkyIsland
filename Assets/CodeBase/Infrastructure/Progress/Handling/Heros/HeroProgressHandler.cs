using Gameplay.Heros;

namespace Infrastructure.Progress.Handling.Heros
{
    public class HeroProgressHandler : IProgressHandler
    {
        public ISavableHero Hero { get; private set; }
        private HeroProgress _progress;

        public void Init(HeroProgress progress) => 
            _progress = progress;

        public void RegisterHero(ISavableHero hero) => 
            Hero = hero;

        public void SetValuesFromProgress()
        {
            if (_progress.IsEmpty == false) 
                Hero.Health = _progress.CurrentHealth;
        }

        public void WriteValuesToProgress()
        {
            _progress.IsEmpty = false;
            _progress.CurrentHealth = Hero.Health;
        }
    }
}