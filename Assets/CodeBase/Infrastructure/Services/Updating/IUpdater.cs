using System;

namespace Infrastructure.Services.Updating
{
    public interface IUpdater
    {
        event Action<float> Updated;
        event Action<float> FixedUpdated;
        event Action<float> LateUpdated;

        event Action<float> NotPausingUpdated;
        
        void StartUpdating();
        void StopUpdating();
    }
}