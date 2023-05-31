using Gameplay.Dying;

namespace Infrastructure.Services.PlayerDeathService
{
    public interface IPlayerDeathService
    {
        void Initialize(IDeath playerDeath);
    }
}