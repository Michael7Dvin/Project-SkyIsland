using System;
using Common.Observable;

namespace Gameplay.Movement.GroundTypeTracking
{
    public interface IGroundTypeTracker : IDisposable
    {
        IReadOnlyObservable<GroundType> CurrentGroundType { get; }
    }
}