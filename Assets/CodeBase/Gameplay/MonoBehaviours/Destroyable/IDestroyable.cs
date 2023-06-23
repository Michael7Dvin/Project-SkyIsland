using System;

namespace Gameplay.MonoBehaviours.Destroyable
{
    public interface IDestroyable
    {
        event Action Destroyed;
    }
}