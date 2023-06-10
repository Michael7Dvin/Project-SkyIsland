using System;
using Common.Observable;
using UnityEngine;

namespace Infrastructure.Services.Input.Handlers.Hero
{
    public interface IHeroInput : IInputHandler
    {
        IReadOnlyObservable<Vector3> HorizontalMoveDirection { get; }
        event Action Jumped;
    }
}