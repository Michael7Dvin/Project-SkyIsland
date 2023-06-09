namespace Infrastructure.Services.Input.Handlers
{
    public interface IInputHandler
    {
        InputHandlerType Type { get; }
    
        
        void Init();

        void Enable();
        void Disable();
    }
}