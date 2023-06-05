namespace Infrastructure.Services.Logger
{
    public interface ICustomLogger
    {
        void Log(string message);
        void LogError(string message);
    }
}