using System;
using UnityEngine;

namespace Gameplay.Movement
{
    public interface IMovement : IDisposable
    {
        void Teleport(Vector3 position);
    }
}