using System;

namespace Infrastructure.Services.Input.Handlers.Utility
{
    public interface IUtilityInput : IInputHandler, IDisposable
    {
        event Action Paused;
    }
}