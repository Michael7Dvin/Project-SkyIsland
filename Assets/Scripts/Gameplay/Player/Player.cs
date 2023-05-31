using System;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;

namespace Gameplay.Player
{
    public class Player : IDisposable
    {
        private readonly IPlayerMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;
        
        private readonly IGameObjectLifeCycleNotifier _gameObjectLifeCycleNotifier;

        public Player(IPlayerMovement movement, IInjuryProcessor injuryProcessor, IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier)
        {
            _movement = movement;
            _injuryProcessor = injuryProcessor;

            _gameObjectLifeCycleNotifier = gameObjectLifeCycleNotifier;

            _gameObjectLifeCycleNotifier.Destroyed += Dispose;
        }

        public void Dispose()
        {
            _movement.Dispose();
            _injuryProcessor.Dispose();
            _gameObjectLifeCycleNotifier.Destroyed -= Dispose;
        }
    }
}