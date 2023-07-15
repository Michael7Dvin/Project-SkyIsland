using Gameplay.Dying;

namespace Gameplay.Services.HeroDeath
{
    public interface IHeroDeathService
    {
        void Init(IDeath heroDeath);
    }
}