using Gameplay.Dying;

namespace Gameplay.PlayerDeathService
{
    public interface IPlayerDeathService
    {
        void Initialize(IDeath playerDeath);
    }
}