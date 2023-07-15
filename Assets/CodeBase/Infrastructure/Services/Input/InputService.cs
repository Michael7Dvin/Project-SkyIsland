using Infrastructure.Services.Input.Handlers.Cameras;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.UI;
using Infrastructure.Services.Input.Handlers.Utility;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private PlayerInput _input;
        
        public void Init()
        {
            _input = new PlayerInput();
            _input.Enable();

            HeroInput hero = new HeroInput(_input.Movement);
            CameraInput camera = new CameraInput(_input.Camera);
            UIInput ui = new UIInput(_input.UI);
            UtilityInput utility = new UtilityInput(_input.Utility);

            hero.Init();
            utility.Init();

            Hero = hero;
            Camera = camera;
            UI = ui;
            Utility = utility;
        }

        public InputActionAsset InputActionAsset => _input.asset;

        public IHeroInput Hero { get; private set; }
        public ICameraInput Camera { get; private set; }
        public IUIInput UI { get; private set; }
        public IUtilityInput Utility { get; private set; }

        public void Dispose()
        {
            Hero.Dispose();
            Utility.Dispose();
        }

        public void EnableAllInput()
        {
            Hero.Enable();
            Camera.Enable();
            UI.Enable();
            Utility.Enable();
        }

        public void DisableAllInput()
        {
            Hero.Disable();
            Camera.Disable();
            UI.Disable();
            Utility.Disable();
        }
    }
}