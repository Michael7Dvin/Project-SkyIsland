public interface IStateWithArguments<in TArgs> : IExitableState
{
    public void Enter(TArgs args);
}