using Common.FSM;
using Infrastructure.GameFSM;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace UI.Services.Mediating
{
    public class Mediator : IMediator
    {
        private readonly IWindowsOperator _windowsOperator;
        private readonly IGameStateMachine _gameStateMachine;

        public Mediator(IWindowsOperator windowsOperator, IGameStateMachine gameStateMachine)
        {
            _windowsOperator = windowsOperator;
            _gameStateMachine = gameStateMachine;
        }

        public void OpenUIWindow(WindowType type) => 
            _windowsOperator.OpenWindow(type);
        
        public void EnterGameState<TState>() where TState : IState => 
            _gameStateMachine.EnterState<TState>();

        public void EnterGameState<TState, TArgs>(TArgs args) where TState : IStateWithArguments<TArgs> => 
            _gameStateMachine.EnterState<TState, TArgs>(args);
    }
}