using System;
using UnityEngine;

namespace Gameplay.Heros.Movement
{
    public interface IMovement : IDisposable
    {
        void Teleport(Vector3 position);
    }
}