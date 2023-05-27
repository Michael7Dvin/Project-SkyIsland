using System;

namespace Gameplay.MonoBehaviours
{
    public interface IGameObjectLifeCycleNotifier
    {
        event Action Destroyed;
    }
}