using System;

namespace Infrastructure.Services.Input.Handlers.Utility
{
    public interface IUtilityInput
    {
        event Action Paused;
    }
}