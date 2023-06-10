namespace Infrastructure.Services.Input.Handlers.UI
{
    public class UIInput : IUIInput
    {
        private readonly PlayerInput.UIActions _actions;

        public UIInput(PlayerInput.UIActions actions)
        {
            _actions = actions;
        }

        public void Enable() => 
            _actions.Enable();

        public void Disable() => 
            _actions.Disable();
    }
}