namespace Infrastructure.Services.Input.Handlers
{
    public interface IInputHandler
    {
        InputHandlerType Type { get; }

        void Enable();
        void Disable();
    }
}