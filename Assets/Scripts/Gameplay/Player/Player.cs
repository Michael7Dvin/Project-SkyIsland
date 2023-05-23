using Gameplay.BodyEnvironmentObserving;
using Gameplay.Player.Movement;

namespace Gameplay.Player
{
    public class Player
    {
        private readonly PlayerMovement _movement;
        private readonly IBodyEnvironmentObserver _bodyEnvironmentObserver;

        public Player(PlayerMovement movement, IBodyEnvironmentObserver bodyEnvironmentObserver)
        {
            _movement = movement;
            _bodyEnvironmentObserver = bodyEnvironmentObserver;
        }
    }
}