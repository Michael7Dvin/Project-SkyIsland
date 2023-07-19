using System;
using Gameplay.Dying;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.Movement;
using UnityEngine;

namespace Gameplay.Heros
{
    public class Hero : IDisposable
    {
        private readonly IMovement _movement;
        private readonly InjuryProcessor _injuryProcessor;

        public Hero(IMovement movement,
            InjuryProcessor injuryProcessor,
            Transform cameraFollowPoint,
            IDeath death,
            Destroyable destroyable,
            HeroProgressDataProvider progressDataProvider)
        {
            _movement = movement;
            _injuryProcessor = injuryProcessor;

            CameraFollowPoint = cameraFollowPoint;
            Death = death;
            Destroyable = destroyable;
            ProgressDataProvider = progressDataProvider;

            Destroyable.Destroyed += Dispose;
        }

        public Transform CameraFollowPoint { get; }
        public IDeath Death { get; }
        public Destroyable Destroyable { get; }
        public HeroProgressDataProvider ProgressDataProvider { get; }

        public void Dispose()
        {
            Destroyable.Destroyed -= Dispose;
            
            _movement.Dispose();
            _injuryProcessor.Dispose();
            Death.Dispose();
        }
    }
}