using System;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Player.Movement;

namespace Gameplay.Player
{
    public class Player : IDisposable
    {
        private readonly IPlayerMovement _movement;
        private readonly IBodyEnvironmentObserver _bodyEnvironmentObserver;
        private readonly IGameObjectLifeCycleNotifier _gameObjectLifeCycleNotifier;

        public Player(IPlayerMovement movement, IBodyEnvironmentObserver bodyEnvironmentObserver, IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier)
        {
            _movement = movement;
            _bodyEnvironmentObserver = bodyEnvironmentObserver;
            _gameObjectLifeCycleNotifier = gameObjectLifeCycleNotifier;

            _gameObjectLifeCycleNotifier.Destroyed += Dispose;
        }

        public void Dispose()
        {
            _movement.Dispose();
            _bodyEnvironmentObserver.Dispose();
            _gameObjectLifeCycleNotifier.Destroyed -= Dispose;
        }
    }
}