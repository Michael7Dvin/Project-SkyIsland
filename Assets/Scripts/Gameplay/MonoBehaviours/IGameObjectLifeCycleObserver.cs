using System;

namespace Gameplay.MonoBehaviours
{
    public interface IGameObjectLifeCycleObserver
    {
        event Action Destroyed;
    }
}