using System;
using Infrastructure.Services.Input.Handlers.Camera;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.UI;
using Infrastructure.Services.Input.Handlers.Utility;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Service
{
    public interface IInputService : IDisposable
    {
        void Init();
        
        InputActionAsset InputActionAsset { get; }
        
        IHeroInput Hero { get; }
        ICameraInput Camera { get; }
        IUIInput UI { get; }
        IUtilityInput Utility { get; }

        void EnableAllInput();
    }
}