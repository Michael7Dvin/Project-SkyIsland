using Infrastructure.GameFSM.States;
using UI.Services.Mediating;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowLogic
    {
        private readonly IMediator _mediator;

        public DeathWindowLogic(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Respawn()
        {
        }
        
        public void ReturnToMainMenu() => 
            _mediator.EnterGameState<MainMenuState>();
    }
}