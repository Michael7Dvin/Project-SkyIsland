using System;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public interface ISlopeCalculator : IDisposable
    {
        Vector3 SlopeDirection { get; }
        float SlopeAngle { get; }
    }
}