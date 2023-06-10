using Common.FSM;
using UI.Windows;

namespace UI.Services.Mediating
{
    public interface IMediator
    {
        void OpenUIWindow(WindowType type);
        void CloseApp();
        void EnterGameState<TState>() where TState : IState;
        void EnterGameState<TState, TArgs>(TArgs args) where TState : IStateWithArguments<TArgs>;
    }
}