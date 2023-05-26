using System;
using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;

namespace Gameplay.Player
{
    public class Player : IDisposable
    {
        private readonly IPlayerMovement _movement;
        private readonly IGameObjectLifeCycleObserver _gameObjectLifeCycleObserver;

        public Player(IPlayerMovement movement, IGameObjectLifeCycleObserver gameObjectLifeCycleObserver)
        {
            _movement = movement;
            _gameObjectLifeCycleObserver = gameObjectLifeCycleObserver;

            _gameObjectLifeCycleObserver.Destroyed += Dispose;
        }

        public void Dispose()
        {
            _movement.Dispose();
            _gameObjectLifeCycleObserver.Destroyed -= Dispose;
        }
    }
}