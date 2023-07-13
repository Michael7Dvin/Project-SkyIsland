using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours.Destroyable
{
    public interface IDestroyable
    {
        event Action Destroyed;
    }
}