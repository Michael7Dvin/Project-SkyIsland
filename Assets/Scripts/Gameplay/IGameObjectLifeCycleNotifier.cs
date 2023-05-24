using System;

namespace Gameplay
{
    public interface IGameObjectLifeCycleNotifier
    {
        event Action Awoke;
        event Action Started;
        event Action Enabled;
        event Action Disabled;
        event Action Destroyed;
    }
}