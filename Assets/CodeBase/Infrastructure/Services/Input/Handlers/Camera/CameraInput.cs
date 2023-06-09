namespace Infrastructure.Services.Input.Handlers.Camera
{
    public class CameraInput : ICameraInput, IInputHandler
    {
        private readonly PlayerInput.CameraActions _actions;

        public CameraInput(PlayerInput.CameraActions actions)
        {
            _actions = actions;
        }

        public InputHandlerType Type => InputHandlerType.Camera;
        
        public void Enable() => 
            _actions.Enable();

        public void Disable() => 
            _actions.Disable();
    }
}