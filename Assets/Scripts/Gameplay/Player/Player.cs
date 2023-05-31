using System;
using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;

namespace Gameplay.Player
{
    public class Player : IDisposable
    {
        private readonly IPlayerMovement _movement;

        private readonly IGameObjectLifeCycleNotifier _gameObjectLifeCycleNotifier;

        public Player(IPlayerMovement movement, IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier)
        {
            _movement = movement;

            _gameObjectLifeCycleNotifier = gameObjectLifeCycleNotifier;

            _gameObjectLifeCycleNotifier.Destroyed += Dispose;
        }

        public void Dispose()
        {
            _movement.Dispose();
            _gameObjectLifeCycleNotifier.Destroyed -= Dispose;
        }
    }
}