using Gameplay.Player;

namespace Infrastructure.Services.Configuration
{
    public interface IConfigProvider
    {
        PlayerConfig GetForPlayer();
    }
}