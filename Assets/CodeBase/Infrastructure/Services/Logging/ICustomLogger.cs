namespace Infrastructure.Services.Logging
{
    public interface ICustomLogger
    {
        void Log(string message);
        void LogError(string message);
    }
}