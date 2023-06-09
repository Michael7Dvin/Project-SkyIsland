using Gameplay.Dying;

namespace Gameplay.HeroDeathService
{
    public interface IHeroDeathService
    {
        void Init(IDeath playerDeath);
    }
}