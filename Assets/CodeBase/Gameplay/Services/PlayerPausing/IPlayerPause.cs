using System;
using Zenject;

namespace Gameplay.Services.PlayerPausing
{
    public interface IPlayerPause : IDisposable, IInitializable
    {
    }
}