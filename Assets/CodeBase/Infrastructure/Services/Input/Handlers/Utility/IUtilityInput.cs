using System;

namespace Infrastructure.Services.Input.Handlers.Utility
{
    public interface IUtilityInput : IInputHandler
    {
        event Action Paused;
    }
}