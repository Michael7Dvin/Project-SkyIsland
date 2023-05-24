using Common.Observable;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        public IReadOnlyObservable<Vector2> HorizontalDirection { get; }
        public IReadOnlyObservable<float> HorizontalMagnitude { get; }
    }
}