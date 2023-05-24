using System;

namespace Common.FSM
{
    public interface IExitableState : IDisposable
    {
        void Exit();
    }
}