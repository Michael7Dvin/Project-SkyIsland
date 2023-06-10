using Infrastructure.GameFSM.States;
using UI.Services.Mediating;
using UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations
{
    public class DeathWindow : BaseWindow
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _mainMenuButton;
        
        private IMediator _mediator;

        public void Construct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override WindowType Type => WindowType.Death;

        protected override void SubscribeOnButtons()
        {
            _respawnButton.onClick.AddListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected override void UnsubscribeFromButtons()
        {
            _respawnButton.onClick.RemoveListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
        }

        private void OnRespawnButtonClick()
        {
        }

        public void OnMainMenuButtonClick() =>
            _mediator.EnterGameState<MainMenuState>();
    }
}