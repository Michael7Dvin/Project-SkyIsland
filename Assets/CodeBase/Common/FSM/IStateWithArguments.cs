namespace Common.FSM
{
    public interface IStateWithArguments<in TArgs> : IExitableState
    {
        void Enter(TArgs argument);
    }
}