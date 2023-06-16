using Infrastructure.GameFSM.States;
using UI.Services.Mediating;

namespace UI.Windows.Implementations.MainMenu
{
    public class MainMenuWindowLogic
    {
        private readonly IMediator _mediator;

        public MainMenuWindowLogic(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OpenSaveSlotSelection() =>
            _mediator.OpenUIWindow(WindowType.SaveSelection);

        public void OpenOptions()
        {
        }

        public void Quit() =>
            _mediator.EnterGameState<QuitState>();
    }
}