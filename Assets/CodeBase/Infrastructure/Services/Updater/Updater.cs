using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Updater
{
    public class Updater : IUpdater, ITickable, IFixedTickable, ILateTickable
    {
        public event Action<float> Updated;
        public event Action<float> FixedUpdated;
        public event Action<float> LateUpdated;
        
        public void Tick() => 
            Updated?.Invoke(Time.deltaTime);

        public void FixedTick() => 
            FixedUpdated?.Invoke(Time.fixedDeltaTime);

        public void LateTick() => 
            LateUpdated?.Invoke(Time.deltaTime);
    }
}