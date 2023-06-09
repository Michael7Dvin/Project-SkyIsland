using System.Linq;
using Infrastructure.Services.Input.Handlers;
using Infrastructure.Services.Input.Handlers.Camera;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.Utility;
using Infrastructure.Services.Logging;

namespace Infrastructure.Services.Input.Service
{
    public class InputService : IInputService
    {
        private PlayerInput _input;
        
        private IInputHandler[] _inputHandlers;
        
        private HeroInput _hero;
        private CameraInput _camera;
        private UtilityInput _utility;

        private readonly ICustomLogger _logger;

        public InputService(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Init()
        {
            _input = new PlayerInput();
            _input.Enable();

            _hero = new HeroInput(_input.Movement);
            _camera = new CameraInput(_input.Camera);
            _utility = new UtilityInput(_input.Utility);
            
            _hero.Init();
            _utility.Init();

            _inputHandlers = new IInputHandler[] 
                { 
                    _hero, 
                    _camera,
                    _utility, 
                };
        }

        public IHeroInput HeroInput => _hero;
        public ICameraInput CameraInput => _camera;
        public IUtilityInput UtilityInput => _utility;

        public void EnableAllInput()
        {
            foreach (IInputHandler inputHandler in _inputHandlers) 
                inputHandler.Enable();
        }

        public void EnableInput(InputHandlerType type)
        {
            IInputHandler inputHandler = _inputHandlers.FirstOrDefault(handler => handler.Type == type);

            if (inputHandler == null)
            {
                _logger.LogError($"{nameof(IInputHandler)} not found for type: '{type}'");
                return;
            }
            
            inputHandler.Enable();
        }

        public void DisableInput(InputHandlerType type)
        {
            IInputHandler inputHandler = _inputHandlers.FirstOrDefault(handler => handler.Type == type);

            if (inputHandler == null)
            {
                _logger.LogError($"{nameof(IInputHandler)} not found for type: '{type}'");
                return;
            }
            
            inputHandler.Disable();
        }
    }
}