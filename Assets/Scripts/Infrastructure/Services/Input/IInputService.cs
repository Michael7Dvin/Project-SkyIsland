using System;
using Common.Observable;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        public IReadOnlyObservable<Vector3> HorizontalDirection { get; }
        public IReadOnlyObservable<float> HorizontalMagnitude { get; }
        
        event Action Jumped;
    }
}