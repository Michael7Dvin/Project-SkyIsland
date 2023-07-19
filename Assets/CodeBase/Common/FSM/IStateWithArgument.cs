namespace Common.FSM
{
    public interface IStateWithArgument<in TArgument> : IExitableState
    {
        void Enter(TArgument argument);
    }
}