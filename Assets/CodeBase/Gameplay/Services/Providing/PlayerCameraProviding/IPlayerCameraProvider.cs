﻿using Common.Observable;
using Gameplay.PlayerCameras;

namespace Gameplay.Services.Providing.PlayerCameraProviding
{
    public interface IPlayerCameraProvider
    {
        public IReadOnlyObservable<PlayerCamera> PlayerCamera { get; }

        public void Set(PlayerCamera hero);
    }
}