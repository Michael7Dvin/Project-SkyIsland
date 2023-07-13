using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using UI.Services.Operating;

namespace UI.Windows.Implementations.MainMenu
{
    public class MainMenuWindowLogic
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowsOperator _windowsOperator;

        public MainMenuWindowLogic(IGameStateMachine gameStateMachine, IWindowsOperator windowsOperator)
        {
            _gameStateMachine = gameStateMachine;
            _windowsOperator = windowsOperator;
        }

        public void OpenSaveSlotSelection() =>
            _windowsOperator.OpenWindow(WindowType.SaveSelection);

        public void OpenOptions()
        {
        }

        public void Quit() =>
            _gameStateMachine.EnterState<QuitState>();
    }
}