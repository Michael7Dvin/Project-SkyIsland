using Infrastructure.GameFSM.States;
using UI.Services.Mediating;
using UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations
{
    public class PauseWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _mainMenuButton;

        private IMediator _mediator;

        public void Construct(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override WindowType Type => WindowType.Pause;

        protected override void SubscribeOnButtons()
        {
            _closeButton.onClick.AddListener(Close);
            
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected override void UnsubscribeFromButtons()
        {
            _closeButton.onClick.RemoveListener(Close);
            
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
        }
        
        public void OnOptionsButtonClick()
        {
        }

        public void OnSaveButtonClick()
        {
        }

        public void OnMainMenuButtonClick() =>
            _mediator.EnterGameState<MainMenuState>();
    }
}