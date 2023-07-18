using System;
using Infrastructure.Services.Input.Handlers.Cameras;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.UI;
using Infrastructure.Services.Input.Handlers.Utility;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IDisposable
    {
        void Initialize();
        
        InputActionAsset InputActionAsset { get; }
        
        IHeroInput Hero { get; }
        ICameraInput Camera { get; }
        IUIInput UI { get; }
        IUtilityInput Utility { get; }

        void EnableAllInput();
        void DisableAllInput();
    }
}