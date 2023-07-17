using System;
using Gameplay.Dying;
using Gameplay.Heros.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;

namespace Gameplay.Heros
{
    public class Hero : IDisposable
    {
        private readonly IMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;

        public Hero(IMovement movement,
            IInjuryProcessor injuryProcessor,
            GameObject gameObject,
            Transform cameraFollowPoint,
            IDeath death,
            IDestroyable destroyable,
            IHeroProgressDataProvider progressDataProvider)
        {
            _movement = movement;
            _injuryProcessor = injuryProcessor;

            GameObject = gameObject;
            CameraFollowPoint = cameraFollowPoint;
            Death = death;
            Destroyable = destroyable;
            ProgressDataProvider = progressDataProvider;

            Destroyable.Destroyed += Dispose;
        }

        public GameObject GameObject { get; }
        public Transform CameraFollowPoint { get; }
        public IDeath Death { get; }
        public IDestroyable Destroyable { get; }
        public IHeroProgressDataProvider ProgressDataProvider { get; }

        public void Dispose()
        {
            Destroyable.Destroyed -= Dispose;
            
            _movement.Dispose();
            _injuryProcessor.Dispose();
            Death.Dispose();
        }
    }
}