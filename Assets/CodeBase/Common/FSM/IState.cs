namespace Common.FSM
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}