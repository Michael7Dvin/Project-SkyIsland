using System;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public interface IGroundSpherecaster : IDisposable
    {
        event Action<RaycastHit> SphereCasted;
        event Action SphereCastMissed;
    }
}