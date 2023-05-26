using Gameplay.Movement;
using Gameplay.Player;

namespace Infrastructure.Services.Configuration
{
    public interface IConfigProvider
    {
        MovementConfig GetForMovement();
        PlayerConfig GetForPlayer();
    }
}