using Gameplay.Dying;

namespace Gameplay.HeroDeathService
{
    public interface IHeroDeathService
    {
        void Initialize(IDeath playerDeath);
    }
}