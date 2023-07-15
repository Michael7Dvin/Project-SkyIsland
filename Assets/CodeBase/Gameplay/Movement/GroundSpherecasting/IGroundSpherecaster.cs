using System;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public interface IGroundSphereCaster : IDisposable
    {
        event Action<RaycastHit> SphereCasted;
        event Action SphereCastMissed;
    }
}