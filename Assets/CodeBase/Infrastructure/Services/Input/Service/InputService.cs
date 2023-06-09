using System.Linq;
using Infrastructure.Services.Input.Handlers;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Input.Handlers.Utility;
using Infrastructure.Services.Logging;

namespace Infrastructure.Services.Input.Service
{
    public class InputService : IInputService
    {
        private PlayerInput _input;
        
        private IInputHandler[] _inputHandlers;
        private HeroInput _heroInput;
        private UtilityInput _utilityInput;
        
        private readonly ICustomLogger _logger;

        public InputService(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Init()
        {
            _input = new PlayerInput();
            _input.Enable();

            _heroInput = new HeroInput(_input.Movement);
            _heroInput.Init();
            
            _utilityInput = new UtilityInput(_input.Utility);
            _utilityInput.Init();
            
            _inputHandlers = new IInputHandler[] 
                { 
                    _heroInput, 
                    _utilityInput, 
                };
        }

        public IHeroInput HeroInput => _heroInput;
        public IUtilityInput UtilityInput => _utilityInput;

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