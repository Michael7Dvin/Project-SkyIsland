using UnityEngine;

namespace Infrastructure.Services.Logging
{
    public class CustomLogger : ICustomLogger
    {
        public void Log(string message)
        {
            Debug.Log(message);       
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}