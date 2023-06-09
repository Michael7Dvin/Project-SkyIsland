using Infrastructure.Services.Input.Handlers;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.Utility;

namespace Infrastructure.Services.Input.Service
{
    public interface IInputService
    {
        void Init();
        
        IHeroInput HeroInput { get; }
        IUtilityInput UtilityInput { get; }

        void EnableAllInput();
        
        void EnableInput(InputHandlerType type);
        void DisableInput(InputHandlerType type);
    }
}