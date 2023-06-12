using System;

namespace Gameplay.MonoBehaviours
{
    public interface IDestroyable
    {
        event Action Destroyed;
    }
}