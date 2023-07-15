using System;
using Common.Observable;
using Gameplay.PlayerCameras;
using UnityEngine;

namespace Infrastructure.Services.Input.Handlers.Hero
{
    public interface IHeroInput : IInputHandler, IDisposable
    {
        IReadOnlyObservable<Vector3> HorizontalMoveDirection { get; }
        event Action Jumped;
        void SetHorizontalDirectionAligningCamera(PlayerCamera playerCamera);
    }
}