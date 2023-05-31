using System;

namespace Gameplay.Dying
{
    public interface IDeath : IDisposable
    {
        event Action Died;
    }
}