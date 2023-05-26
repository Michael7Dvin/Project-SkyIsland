using System;

namespace Gameplay.Movement.SlopeCalculation
{
    public interface ISlopeCalculator : IDisposable
    {
        float SlopeAngle { get; }
    }
}