using Infrastructure.GameFSM.States;
using UI.Services.Mediating;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowLogic
    {
        private readonly IMediator _mediator;

        public PauseWindowLogic(IMediator mediator)
        {
            _mediator = mediator; }

        public void OpenOptions()
        {
        }

        public void SaveProgess()
        {
        }
        
        public void ReturnToMainMenu() => 
            _mediator.EnterGameState<MainMenuState>();
    }
}