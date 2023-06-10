using Common.FSM;
using Infrastructure.GameFSM;
using Infrastructure.Services.AppClosing;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace UI.Services.Mediating
{
    public class Mediator : IMediator
    {
        private readonly IWindowsService _windowsService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAppCloser _appCloser;

        public Mediator(IWindowsService windowsService, IGameStateMachine gameStateMachine, IAppCloser appCloser)
        {
            _windowsService = windowsService;
            _gameStateMachine = gameStateMachine;
            _appCloser = appCloser;
        }

        public void OpenUIWindow(WindowType type) => 
            _windowsService.OpenWindow(type);

        public void CloseApp() => 
            _appCloser.Close();

        public void EnterGameState<TState>() where TState : IState => 
            _gameStateMachine.EnterState<TState>();

        public void EnterGameState<TState, TArgs>(TArgs args) where TState : IStateWithArguments<TArgs> => 
            _gameStateMachine.EnterState<TState, TArgs>(args);
    }
}